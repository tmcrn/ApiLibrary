using ApiLibrary.DTO.Book.Request;
using ApiLibrary.DTO.Book.Response;
using FastEndpoints;

namespace ApiLibrary.Endpoints.Book;

public class CreateBookEndpoint(LibraryDbContext libraryDbContext) : Endpoint<CreateBookDto, GetBookDto>
{
    public override void  Configure()
    {
        Post("/api/books");
    }   

    public override async Task HandleAsync(CreateBookDto req, CancellationToken ct)
    {
        var book = new Models.Book
        {
            Title = req.Title,
            ISBN = req.ISBN,
            ReleaseYear = req.ReleaseYear,
            AuthorId = req.AuthorId
        };

        libraryDbContext.Books.Add(book);
        await libraryDbContext.SaveChangesAsync(ct);

        GetBookDto result = new GetBookDto()
        {
            Id = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            ReleaseYear = book.ReleaseYear,
            AuthorId = book.AuthorId
        };

        await Send.OkAsync(result, ct);
    }

}