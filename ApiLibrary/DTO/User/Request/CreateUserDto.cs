namespace ApiLibrary.DTO.User.Request;

public class CreateUserDto
{
    public required string FirstName { get; set; }
    public required string LastName  { get; set; }
    public required string Email     { get; set; }
    public DateOnly? BirthDate       { get; set; }
}