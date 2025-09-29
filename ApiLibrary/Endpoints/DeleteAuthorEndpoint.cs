using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints;

public class DeleteAuthorEndpoint(LibraryDbContext db) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/api/authors/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");

        var author = await db.Authors.FirstOrDefaultAsync(a => a.Id == id, ct);

        db.Authors.Remove(author);
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(ct);
    }
}