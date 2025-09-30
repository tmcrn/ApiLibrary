using ApiLibrary.DTO;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints;

public class GetUserEndpoint (LibraryDbContext libraryDbContext): EndpointWithoutRequest<GetUserDto>
{
    public override void Configure()
    {
        Get("api/users/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");

        var user = await libraryDbContext.Users.Where(u => u.Id == id).Select(u => new GetUserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                BirthDate = u.BirthDate
            })
            .FirstOrDefaultAsync(ct);

        await Send.OkAsync(user, ct);
    }
}