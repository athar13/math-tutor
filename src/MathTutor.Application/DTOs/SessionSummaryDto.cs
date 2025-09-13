namespace MathTutor.Application.DTOs;

public class SessionSummaryDto
{
    public Guid SessionId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public int TotalQuestions { get; set; }
    public int QuestionsAnswered { get; set; }
    public int CorrectAnswers { get; set; }
    public int MaxStreak { get; set; }
    public bool IsActive { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
}


