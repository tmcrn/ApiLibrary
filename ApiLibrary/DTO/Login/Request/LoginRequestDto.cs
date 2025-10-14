namespace ApiLibrary.DTO.Login.Request;

public class LoginRequestDto
{
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}