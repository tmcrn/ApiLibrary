using ApiLibrary.DTO.Book.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Book;

public class GetAllBooksEndpoint(LibraryDbContext libraryDbContext): EndpointWithoutRequest<List<GetBookDto>>
{
    public override void Configure()
    {
        Get("/api/books");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await libraryDbContext.Books
            .Select(b => new GetBookDto
            {
                Id = b.Id,
                Title = b.Title,
                AuthorId = b.AuthorId,
                ReleaseYear = b.ReleaseYear,
                ISBN = b.ISBN,
                AuthorFullName = $"{b.Author!.Firstname} {b.Author!.Name}".Trim()
            })
            .ToListAsync(ct);

        await Send.OkAsync(result, ct);
    }
}