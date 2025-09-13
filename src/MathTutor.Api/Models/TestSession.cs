namespace MathTutor.Api.Models;

public class TestSession
{
    public long Id { get; set; }
    public Guid TestSessionIdentifier { get; set; } = Guid.NewGuid();
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int TotalQuestions { get; set; }
    public int QuestionsAnswered { get; set; }
    public int CorrectAnswers { get; set; }
    public bool IsActive { get; set; }

    // Track streaks inside a session
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }

    // Navigation
    public List<TestSessionQuestion> Questions { get; set; } = new();
}