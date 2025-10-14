using ApiLibrary.DTO.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Loan;

public class GetAllLoansEndpoint(LibraryDbContext libraryDbContext): EndpointWithoutRequest<List<GetLoanDto>>
{
    public override void Configure()
    {
        Get("/api/loans");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var loans = await libraryDbContext.Loans
            .Include(b => b.Book)
            .ThenInclude(a => a.Author)
            .Include(u=>u.User)
            .ToListAsync(ct);

        var result = loans.Select(loan => new GetLoanDto
        {
            Id = loan.Id,
            BookId = loan.BookId,
            UserId = loan.UserId,
            BookAuthorId = loan.Book.AuthorId,
            Date = loan.Date,
            PlannedReturningDate = loan.PlannedReturningDate,
            EffectiveReturningDate = loan.EffectiveReturningDate,
            BookTitle = loan.Book.Title,
            UserFirstName = loan.User.FirstName,
            UserLastName = loan.User.LastName,
            UserEmail = loan.User.Email,
            UserBirthDate = loan.User.BirthDate,
            BookReleaseYear = loan.Book.ReleaseYear,
            BookISBN = loan.Book.ISBN,
            BookAuthorFirstname = loan.Book.Author.Firstname,
            BookAuthorName = loan.Book.Author.Firstname
        }).ToList();

        await Send.OkAsync(result, ct);
    }
}