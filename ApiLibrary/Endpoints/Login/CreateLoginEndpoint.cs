using ApiLibrary.DTO.Login.Request;
using ApiLibrary.DTO.Login.Response;
using FastEndpoints;

namespace ApiLibrary.Endpoints.Login;

public class CreateLoginEndpoint (LibraryDbContext libraryDbContext): Endpoint<CreateLoginDto, GetLoginDto>
{
    public override void Configure()
    {
        Post("api/logins");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateLoginDto req, CancellationToken ct)
    {
        
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(req.PasswordHash);
        
        var login = new Models.Login
        {
            Username = req.Username,
            FuulName = req.FuulName,
            PasswordHash = passwordHash,
            Role = req.Role
        };
        
        libraryDbContext.Logins.Add(login);
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