namespace ApiLibrary.DTO;

public class UpdateUserDto
{
    public int Id  { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateOnly? BirthDate { get; set; }
}