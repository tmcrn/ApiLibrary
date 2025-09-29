using System.ComponentModel.DataAnnotations;

namespace ApiLibrary.Models;

public class User
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(100)] public required string FirstName { get; set; }
    [Required, MaxLength(100)] public string LastName { get; set; }
    [Required, MaxLength(255)] public string Email { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Loan? Loans { get; set; }
}