using ApiLibrary.DTO.Login.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Login;

public class GetAllLoginsEndpoint (LibraryDbContext libraryDbContext):EndpointWithoutRequest<List<GetLoginDto>>
{
    public override void Configure()
    {
        Get("api/logins"); 
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var login = await libraryDbContext.Logins.ToListAsync(ct);
        
        var result = login.Select(login => new GetLoginDto
        {
            Id = login.Id,
            Username = login.Username,
            FuulName = login.FuulName,
            PasswordHash = login.PasswordHash,
            Role = login.Role
        }).ToList();
        
        await Send.OkAsync(result, ct);
    }
}