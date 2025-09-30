using ApiLibrary.DTO;
using ApiLibrary.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace ApiLibrary.Endpoints;

public class UpdateAuthorEndpoint(LibraryDbContext libraryDbContext):Endpoint<UpdateAuthorDto, GetAuthorDto>
{
    public override void Configure()
    {
        Put("api/authors{id}", a => new{a.Id});
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateAuthorDto req, CancellationToken ct)
    {        
        var author = await libraryDbContext.Authors.FirstOrDefaultAsync(a => a.Id == req.Id, ct);
        
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
