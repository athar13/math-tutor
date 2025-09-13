namespace MathTutor.Domain.Entities;

public class TestSession
{
    public long Id { get; set; }
    public Guid TestSessionIdentifier { get; set; } = Guid.NewGuid();
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public int TotalQuestions { get; set; }
    public int QuestionsAnswered { get; set; } = 0;
    public int CorrectAnswers { get; set; } = 0;
    public int CurrentStreak { get; set; } = 0;
    public int MaxStreak { get; set; } = 0;
    public bool IsActive { get; set; } = true;

    public List<TestSessionQuestion> Questions { get; set; } = new();
}
