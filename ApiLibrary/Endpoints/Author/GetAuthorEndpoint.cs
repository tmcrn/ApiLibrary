using ApiLibrary.DTO.Author.Request;
using ApiLibrary.DTO.Author.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Author;

public class GetAuthorEndpoint(LibraryDbContext libraryDbContext) : Endpoint<IdAuthorDto, GetAuthorDto>
{
    public override void Configure()
    {
        Get("/api/authors/{Id}");
    }

    public override async Task HandleAsync(IdAuthorDto req, CancellationToken ct)
    {
        var author = await libraryDbContext.Authors
            .Where(a => a.Id == req.Id)
            .Select(a => new GetAuthorDto
            {
                Id = a.Id,
                Firstname = a.Firstname,
                Name = a.Name
            })
            .FirstOrDefaultAsync(ct);
    }
}