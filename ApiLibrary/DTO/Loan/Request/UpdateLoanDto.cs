namespace ApiLibrary.DTO.Loan.Request;

public class UpdateLoanDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly PlannedReturningDate { get; set; }
    public DateOnly? EffectiveReturningDate { get; set; }
}