
using ApiLibrary.DTO.Loan.Request;
using ApiLibrary.DTO.Loan.Response;
using FastEndpoints;

namespace ApiLibrary.Endpoints.Loan;

public class CreateLoanEndpoint(LibraryDbContext libraryDbContext) : Endpoint<CreateLoanDto, GetLoanDto>
{
    public override void  Configure()
    {
        Post("/loans");
        AllowAnonymous();
    }   

    public override async Task HandleAsync(CreateLoanDto req, CancellationToken ct)
    {
        var loan = new Models.Loan
        {
            BookId = req.BookId,
            UserId = req.UserId,
            Date = req.Date,
            PlannedReturningDate = req.Date.AddMonths(2)
        };

        libraryDbContext.Loans.Add(loan);
        await libraryDbContext.SaveChangesAsync(ct);

        GetLoanDto result = new GetLoanDto()
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