using ApiLibrary.DTO.Login.Request;
using ApiLibrary.DTO.Login.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Login;

public class UpdateLoginEndpoint(LibraryDbContext libraryDbContext):Endpoint<UpdateLoginDto, GetLoginDto>
{
    public override void Configure()
    {
        Put("api/logins/{@Id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateLoginDto req, CancellationToken ct)
    {
        var login = await libraryDbContext.Logins.FirstOrDefaultAsync(u => u.Id == req.Id, ct);
        
        login.Username = req.Username;
        login.FuulName = req.FuulName;
        login.PasswordHash = req.PasswordHash;
        login.Role = req.Role;
        
        await libraryDbContext.SaveChangesAsync(ct);

        GetLoginDto result = new GetLoginDto()
        {
            Id = login.Id,
            Username = login.Username,
            FuulName = login.FuulName,
            PasswordHash = login.PasswordHash,
            Role = login.Role
        };

        await Send.OkAsync(result, ct);
    }
}