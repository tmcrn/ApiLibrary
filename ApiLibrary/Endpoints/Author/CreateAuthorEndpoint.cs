using ApiLibrary.DTO.Author.Request;
using ApiLibrary.DTO.Author.Response;
using FastEndpoints;

namespace ApiLibrary.Endpoints.Author;

public class CreateAuthorEndpoint(LibraryDbContext libraryDbContext) : Endpoint<CreateAuthorDto, GetAuthorDto>
{
    public override void  Configure()
    {
        Post("/authors");
        AllowAnonymous();
    }   

    public override async Task HandleAsync(CreateAuthorDto req, CancellationToken ct)
    {
        var author = new Models.Author
        {
            Firstname = req.Firstname,
            Name = req.Name
        };

        libraryDbContext.Authors.Add(author);
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