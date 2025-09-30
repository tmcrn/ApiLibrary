using ApiLibrary.DTO;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints;

public class UpdateUserEndpoint(LibraryDbContext libraryDbContext):Endpoint<UpdateUserDto, GetUserDto>
{
    public override void Configure()
    {
        Put("api/users/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateUserDto req, CancellationToken ct)
    {
        var id = Route<int>("id");
        
        var user = await libraryDbContext.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
        
        user.FirstName = req.FirstName;
        user.LastName = req.LastName;
        user.Email = req.Email;
        user.BirthDate = req.BirthDate;
        
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