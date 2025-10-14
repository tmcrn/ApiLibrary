using ApiLibrary.DTO.Loan.Request;
using ApiLibrary.DTO.Loan.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Loan;

public class GetLoanEndpoint(LibraryDbContext libraryDbContext) 
    : Endpoint<IdLoanDto, GetLoanDto>
{
    public override void Configure()
    {
        Get("/api/loans/{Id}");
    }

    public override async Task HandleAsync(IdLoanDto req, CancellationToken ct)
    {
        var result = await libraryDbContext.Loans
            .Include(b => b.Book)
            .ThenInclude(a => a.Author)
            .Include(u => u.User)
            .Select(loan => new GetLoanDto
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
                BookAuthorName = loan.Book.Author.Name
            })
            .FirstOrDefaultAsync(l => l.Id == req.Id, ct);

        await Send.OkAsync(result, ct);
    }
}