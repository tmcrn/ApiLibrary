using ApiLibrary.DTO.Loan.Request;
using ApiLibrary.DTO.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Loan;

public class GetLoanEndpoint(LibraryDbContext libraryDbContext) : Endpoint<IdLoanDto, GetLoanDto>
{
    public override void Configure()
    {
        Get("/api/loans/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(IdLoanDto req, CancellationToken ct)
    {
        var loan = await libraryDbContext.Loans
            .Include(a => a.Book)
            .Where(a => a.Id == req.Id)
            .Select(a => new GetLoanDto
            {
                Id = a.Id,
                BookId = a.BookId,
                UserId = a.UserId,
                Date = a.Date,
                PlannedReturningDate = a.PlannedReturningDate,
                EffectiveReturningDate = a.EffectiveReturningDate,
                BookTitle = a.Book.Title
            })
            .FirstOrDefaultAsync(ct);
    }
}