using ApiLibrary.DTO.User.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.User;

public class GetAllUsersEndpoint (LibraryDbContext libraryDbContext):EndpointWithoutRequest<List<GetUserDto>>
{
    public override void Configure()
    {
        Get("api/users"); 
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await libraryDbContext.Users.ToListAsync(ct);
        
        var result = user.Select(user => new GetUserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            BirthDate = user.BirthDate
        }).ToList();
        
        await Send.OkAsync(result, ct);
    }
}