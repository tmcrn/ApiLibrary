using ApiLibrary.DTO.Login.Request;
using ApiLibrary.DTO.Login.Response;
using FastEndpoints;
using FastEndpoints.Security;

namespace ApiLibrary.Endpoints.Login;

public class LoginEndpoint : Endpoint<LoginRequestDto, LoginResponseDto>
{
    public override void Configure()
    {
        Post("/api/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequestDto req, CancellationToken ct)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword("monmdp" + "ceciestunsaltquipourtps");
        if (BCrypt.Net.BCrypt.Verify(req.PasswordHash + "ceciestunsaltquipourtps", hashedPassword))
        {
            var jwtToken = JwtBearer.CreateToken(o =>
            {
                o.SigningKey = "UneCléTrèsLongueEtSecrèteDe32CaractèresMinimum";
                o.ExpireAt = DateTime.UtcNow.AddDays(1);
                o.User.Claims.Add(("UserName", req.Username));
                //o.User["UserId"] = "001"; //indexer based claim setting
            });

            LoginResponseDto responseDto = new()
            {
                Token = jwtToken,
            };
            await Send.OkAsync(responseDto, ct);
        }
        else await Send.UnauthorizedAsync(ct);
    }
}