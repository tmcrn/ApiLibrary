namespace ApiLibrary.DTO.User.Request;

public class CreateUserDto
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email     { get; set; }
    public DateOnly? BirthDate       { get; set; }
}