using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints;

public class DeleteUserEndpoint (LibraryDbContext db): EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/api/users/{id}");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");
        
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
        
        db.Users.Remove(user);
        await db.SaveChangesAsync(ct);
        
        await Send.OkAsync(ct);
    }
}