namespace MathTutor.Api.Models;

public class Progress
{
    public long Id { get; set; }
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }

}
