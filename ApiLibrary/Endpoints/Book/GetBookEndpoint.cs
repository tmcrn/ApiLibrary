using ApiLibrary.DTO.Book.Request;
using ApiLibrary.DTO.Book.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Book;

public class GetBookEndpoint(LibraryDbContext libraryDbContext) : Endpoint<IdBookDto, GetBookDto>
{
    public override void Configure()
    {
        Get("/books/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(IdBookDto req, CancellationToken ct)
    {
        var book = await libraryDbContext.Books
            .Include(b => b.Author)
            .Where(b => b.Id == req.Id)
            .Select(b => new GetBookDto
            {
                Id = b.Id,
                Title = b.Title,
                AuthorId = b.AuthorId,
                ReleaseYear = b.ReleaseYear,
                ISBN = b.ISBN,
                AuthorFullName = $"{b.Author.Firstname} {b.Author.Name}".Trim()
            })
            .FirstOrDefaultAsync(ct); 
        await Send.OkAsync(book, ct);
    }
}