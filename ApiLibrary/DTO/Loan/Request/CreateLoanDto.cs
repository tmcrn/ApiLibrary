namespace ApiLibrary.DTO.Loan.Request;

public class CreateLoanDto
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly PlannedReturningDate { get; set; }
}