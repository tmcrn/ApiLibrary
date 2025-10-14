using ApiLibrary;
using FastEndpoints;
using FastEndpoints.Swagger;
using FastEndpoints.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints()
    .AddAuthenticationJwtBearer(s =>
    {
        s.SigningKey = "UneCléTrèsLongueEtSecrèteDe32CaractèresMinimum";
    })
    .AddAuthorization()
    .SwaggerDocument();

builder.Services.AddDbContext<LibraryDbContext>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints()
    .UseSwaggerGen()
    .UseHttpsRedirection();

app.Run();