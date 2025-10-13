using ApiLibrary.DTO.Login.Request;
using ApiLibrary.DTO.Login.Response;
using ApiLibrary.Models;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace ApiLibrary.Endpoints.Login;

public class LoginEndpoint(LibraryDbContext dbContext)
    : Endpoint<LoginRequestDto, LoginResponseDto>
{
    public override void Configure()
    {
        Post("api/login");
        AllowAnonymous(); // pour permettre le login sans token
    }

    public override async Task HandleAsync(LoginRequestDto req, CancellationToken ct)
    {
        // 🔍 1. Chercher l’utilisateur
        var user = await dbContext.Logins.FirstOrDefaultAsync(x => x.Username == req.Username, ct);

        if (user is null)
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        // 🔐 2. Vérifier le mot de passe (avec BCrypt)
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        // ⚔️ 3. Créer les claims du token
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        // 🪄 4. Générer le token JWT
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MaCléSuperSecrète123!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "MonApi",
            audience: "MonApi",
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        // 🏁 5. Retourner la réponse
        await Send.OkAsync(new LoginResponseDto
        {
            Token = tokenString,
            Username = user.Username,
            Role = user.Role
        }, ct);
    }
}
