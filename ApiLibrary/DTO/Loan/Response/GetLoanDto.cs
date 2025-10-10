namespace ApiLibrary.DTO.Loan.Response;

public class GetLoanDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public int BookAuthorId { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly PlannedReturningDate { get; set; }
    public DateOnly? EffectiveReturningDate { get; set; }
    public string? BookTitle { get; set; }
    
    public string? UserFirstName { get; set; }
    public string? UserLastName { get; set; }
    public string? UserEmail { get; set; }
    public DateOnly? UserBirthDate { get; set; }
    
    public int? BookReleaseYear { get; set; }
    public string BookISBN { get; set; }
    
    
    public string? BookAuthorName { get; set; }
    public string? BookAuthorFirstname { get; set; }

}