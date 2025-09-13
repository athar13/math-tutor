namespace MathTutor.Domain.Entities;

public class TestSessionQuestion
{
    public long Id { get; set; }
    public long TestSessionId { get; set; }
    public TestSession TestSession { get; set; } = null!;
    public MathProblem Problem { get; set; } = null!;
    public int? GivenAnswer { get; set; }
    public bool? IsCorrect { get; set; }
}
