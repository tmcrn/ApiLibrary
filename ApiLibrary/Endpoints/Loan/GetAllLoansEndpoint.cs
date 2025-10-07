using ApiLibrary.DTO.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Loan;

public class GetAllLoansEndpoint(LibraryDbContext libraryDbContext): EndpointWithoutRequest<List<GetLoanDto>>
{
    public override void Configure()
    {
        Get("/api/loans");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var loans = await libraryDbContext.Loans.ToListAsync(ct);

        var result = loans.Select(loan => new GetLoanDto
        {
            Id = loan.Id,
            BookId = loan.BookId,
            UserId = loan.UserId,
            Date = loan.Date,
            PlannedReturningDate = loan.PlannedReturningDate,
            EffectiveReturningDate = loan.EffectiveReturningDate,
            BookTitle = loan.Book.Title
        }).ToList();

        await Send.OkAsync(result, ct);
    }
}