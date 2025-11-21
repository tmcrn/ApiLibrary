using ApiLibrary.DTO.Author.Request;
using ApiLibrary.DTO.Author.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints.Author;

public class UpdateAuthorEndpoint(LibraryDbContext libraryDbContext):Endpoint<UpdateAuthorDto, GetAuthorDto>
{
    public override void Configure()
    {
        Put("/authors/{@Id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateAuthorDto req, CancellationToken ct)
    {
        var id = Route<int>("id");
        
        var author = await libraryDbContext.Authors.FirstOrDefaultAsync(a => a.Id == id, ct);
        
        author.Firstname = req.Firstname;
        author.Name = req.Name;
        
        await libraryDbContext.SaveChangesAsync(ct);

        GetAuthorDto result = new GetAuthorDto()
        {
            Id = author.Id,
            Firstname = author.Firstname,
            Name = author.Name
        };

        await Send.OkAsync(result, ct);
    }
}