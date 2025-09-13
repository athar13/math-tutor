namespace MathTutor.Application.DTOs;

public class QuestionDto
{
    public long ProblemId { get; set; }
    public string Question { get; set; } = string.Empty;
    public int Answer { get; set; }
    public int GivenAnswer { get; set; }
    public bool IsCorrect { get; set; }
}