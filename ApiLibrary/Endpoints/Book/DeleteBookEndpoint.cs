using ApiLibrary.DTO.Book.Request;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Book;

public class DeleteBookEndpoint(LibraryDbContext db) : Endpoint<UpdateBookDto>
{
    public override void Configure()
    {
        Delete("/api/books/{@Id}", x => new { x.Id });
    }

    public override async Task HandleAsync(UpdateBookDto req, CancellationToken ct)
    {
        var book = await db.Books.FirstOrDefaultAsync(a => a.Id == req.Id, ct);

        db.Books.Remove(book);
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(ct);
    }
}