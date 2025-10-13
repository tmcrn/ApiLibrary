namespace ApiLibrary.DTO.Login.Request;

public class CreateLoginDto
{
    public string Username { get; set; }
    public string FuulName  { get; set; }
    public string PasswordHash     { get; set; }
    public string Role       { get; set; }
}