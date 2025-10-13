namespace ApiLibrary.DTO.Login.Request;

public class UpdateLoginDto
{
    public int Id                     { get; set; }
    public string Username { get; set; }
    public string FuulName  { get; set; }
    public string PasswordHash     { get; set; }
    public string Role       { get; set; }
}