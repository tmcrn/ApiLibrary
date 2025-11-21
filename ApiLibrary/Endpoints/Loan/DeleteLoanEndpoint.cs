using ApiLibrary.DTO.Loan.Request;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Loan;

public class DeleteLoanEndpoint(LibraryDbContext db) : Endpoint<UpdateLoanDto>
{
    public override void Configure()
    {
        Delete("/loans/{@Id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateLoanDto req, CancellationToken ct)
    {
        var loan = await db.Loans.FirstOrDefaultAsync(a => a.Id == req.Id, ct);

        db.Loans.Remove(loan);
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(ct);
    }
}