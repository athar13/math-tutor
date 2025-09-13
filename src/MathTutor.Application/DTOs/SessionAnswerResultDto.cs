namespace MathTutor.Application.DTOs;

public class SessionAnswerResultDto
{
    public bool Correct { get; set; }
    public int CorrectAnswer { get; set; }
    public int TotalAnswered { get; set; }
    public int TotalQuestions { get; set; }
    public int Score { get; set; }
    public int CurrentStreak { get; set; }
    public int MaxStreak { get; set; }
    public bool SessionCompleted { get; set; }
}
