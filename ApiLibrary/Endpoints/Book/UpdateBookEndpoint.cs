using ApiLibrary.DTO.Author.Request;
using ApiLibrary.DTO.Book.Request;
using ApiLibrary.DTO.Book.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Book;

public class UpdateBookEndpoint(LibraryDbContext libraryDbContext):Endpoint<UpdateBookDto, GetBookDto>
{
    public override void Configure()
    {
        Put("api/books/{@Id}", x => new { x.Id });
    }

    public override async Task HandleAsync(UpdateBookDto req, CancellationToken ct)
    {
        var book = await libraryDbContext.Books.FirstOrDefaultAsync(a => a.Id == req.Id, ct);
        
        book.Title = req.Title;
        book.ReleaseYear = req.ReleaseYear;
        book.AuthorId = req.AuthorId;
        book.ISBN = req.ISBN;
        
        
        await libraryDbContext.SaveChangesAsync(ct);

        GetBookDto result = new GetBookDto()
        {
            Id = book.Id,
            Title = req.Title,
            AuthorId = req.AuthorId,
            ReleaseYear = req.ReleaseYear,
            ISBN = req.ISBN,
            AuthorFullName = $"{book.Author!.Firstname} {book.Author!.Name}".Trim()
        };

        await Send.OkAsync(result, ct);
    }
}