using ApiLibrary.DTO;
using ApiLibrary.Models;
using FastEndpoints;

namespace ApiLibrary.Endpoints;

public class CreateUserEndpoint (LibraryDbContext libraryDbContext): Endpoint<CreateUserDto, GetUserDto>
{
    public override void Configure()
    {
        Post("api/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserDto req, CancellationToken ct)
    {
        var user = new User
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Email = req.Email,
            BirthDate = req.BirthDate
        };
        
        libraryDbContext.Users.Add(user);
        await libraryDbContext.SaveChangesAsync(ct);
        
        GetUserDto result = new GetUserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            BirthDate = user.BirthDate
        };
        
        await Send.OkAsync(result, ct);
    }
}