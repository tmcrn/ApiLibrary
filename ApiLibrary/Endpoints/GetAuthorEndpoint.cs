using ApiLibrary.DTO;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints;

public class GetAuthorEndpoint(LibraryDbContext libraryDbContext): EndpointWithoutRequest<GetAuthorDto>
{
    public override void Configure()
    {
        Get("/api/authors/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");

        var author = await libraryDbContext.Authors.Where(a => a.Id == id).Select(a => new GetAuthorDto
            {
                Id = a.Id,
                Firstname = a.Firstname,
                Name = a.Name
            })
            .FirstOrDefaultAsync(ct);

        await Send.OkAsync(author, ct);
    }
}