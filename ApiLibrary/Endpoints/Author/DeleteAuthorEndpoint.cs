using ApiLibrary.DTO.Author.Request;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Author;

public class DeleteAuthorEndpoint(LibraryDbContext db) : Endpoint<UpdateAuthorDto>
{
    public override void Configure()
    {
        Delete("/api/authors/{@Id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateAuthorDto req, CancellationToken ct)
    {
        var author = await db.Authors.FirstOrDefaultAsync(a => a.Id == req.Id, ct);

        db.Authors.Remove(author);
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(ct);
    }
}