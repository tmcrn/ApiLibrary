using ApiLibrary.DTO.User.Request;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.User;

public class DeleteUserEndpoint (LibraryDbContext db): Endpoint<UpdateUserDto>
{
    public override void Configure()
    {
        Delete("api/users/{@Id}", x => new { x.Id });
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(UpdateUserDto req, CancellationToken ct)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == req.Id, ct);
        
        db.Users.Remove(user);
        await db.SaveChangesAsync(ct);
        
        await Send.OkAsync(ct);
    }
}