namespace MathTutor.Api.Models;

public class TestSessionQuestion
{
    public long Id { get; set; }
    public long TestSessionId { get; set; }
    public TestSession TestSession { get; set; } = null!;

    public long ProblemId { get; set; }
    public MathProblem Problem { get; set; } = null!;

    public int? GivenAnswer { get; set; } = null!;
    public bool? IsCorrect { get; set; } = null!;
}