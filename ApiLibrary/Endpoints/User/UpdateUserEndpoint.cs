using ApiLibrary.DTO.User.Request;
using ApiLibrary.DTO.User.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.User;

public class UpdateUserEndpoint(LibraryDbContext libraryDbContext):Endpoint<UpdateUserDto, GetUserDto>
{
    public override void Configure()
    {
        Put("api/users/{@Id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateUserDto req, CancellationToken ct)
    {
        var user = await libraryDbContext.Users.FirstOrDefaultAsync(u => u.Id == req.Id, ct);
        
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