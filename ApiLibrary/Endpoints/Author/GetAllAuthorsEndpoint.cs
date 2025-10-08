using ApiLibrary.DTO.Author.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Author;

public class GetAllAuthorsEndpoint(LibraryDbContext libraryDbContext): EndpointWithoutRequest<List<GetAuthorDto>>
{
    public override void Configure()
    {
        Get("/api/authors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var authors = await libraryDbContext.Authors.ToListAsync(ct);

        var result = authors
            .Select(author => new GetAuthorDto
            {
                Id = author.Id,
                Firstname = author.Firstname,
                Name = author.Name
            })
            .ToList();

        await Send.OkAsync(result, ct);

    }
}
