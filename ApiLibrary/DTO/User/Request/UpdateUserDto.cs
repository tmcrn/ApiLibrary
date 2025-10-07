namespace ApiLibrary.DTO.User.Request;

public class UpdateUserDto
{
    public int Id                     { get; set; }
    public required string FirstName  { get; set; }
    public required string LastName   { get; set; }
    public required string Email      { get; set; }
    public DateOnly? BirthDate        { get; set; }
}