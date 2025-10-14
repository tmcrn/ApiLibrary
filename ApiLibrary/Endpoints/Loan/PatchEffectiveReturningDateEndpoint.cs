using ApiLibrary.DTO.Loan.Request;
using ApiLibrary.DTO.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Loan;

public class PatchEffectiveReturningDateEndpoint(LibraryDbContext db): Endpoint<EffectiveReturningDateDto, GetLoanDto>
{
    public override void Configure()
    {
        Patch("/api/loans/{@Id}/effectiveReturningDate", x => new { x.Id });
    }

    public override async Task HandleAsync(EffectiveReturningDateDto req, CancellationToken ct)
    {
        var loan = await db.Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .FirstOrDefaultAsync(l => l.Id == req.Id, ct);

        loan.EffectiveReturningDate = req.EffectiveReturningDate;

        await db.SaveChangesAsync(ct);

        var result = new GetLoanDto
        {
            Id = loan.Id,
            BookId = loan.BookId,
            UserId = loan.UserId,
            Date = loan.Date,
            PlannedReturningDate = loan.PlannedReturningDate,
            EffectiveReturningDate = loan.EffectiveReturningDate,
            BookTitle = loan.Book.Title
        };

        await Send.OkAsync(result, ct);
    }
}