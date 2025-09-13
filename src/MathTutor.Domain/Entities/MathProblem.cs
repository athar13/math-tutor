namespace MathTutor.Domain.Entities;

public class MathProblem
{
    public long Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public int Answer { get; set; }
}
