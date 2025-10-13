using System.ComponentModel.DataAnnotations;

namespace ApiLibrary.Models;

public class Login
{
    [Key] public int Id { get; set; }
    [Required] public string Username { get; set; }
    [Required] public string FuulName { get; set; }
    [Required, Length(60,60)] public string PasswordHash { get; set; }
    [Required, Length(24,24)] public string Role { get; set; }
}