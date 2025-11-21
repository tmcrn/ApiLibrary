using ApiLibrary;
using FastEndpoints;
using FastEndpoints.Swagger;
using FastEndpoints.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors(options =>
    {
        options.AddDefaultPolicy(policyBuilder => policyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    })
    .AddFastEndpoints()
    .AddAuthenticationJwtBearer(s =>
    {
        s.SigningKey = "UneCléTrèsLongueEtSecrèteDe32CaractèresMinimum";
    })
    .AddAuthorization()
    .SwaggerDocument(options =>
    {
        options.ShortSchemaNames = true;
    });

builder.Services.AddDbContext<LibraryDbContext>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(options =>
    {
        options.Endpoints.RoutePrefix = "API";
        options.Endpoints.ShortNames = true;
    })
    .UseSwaggerGen()
    .UseHttpsRedirection()
    .UseCors();

app.Run();