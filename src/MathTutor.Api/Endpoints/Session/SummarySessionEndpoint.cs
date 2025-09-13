using MathTutor.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace MathTutor.Api.Endpoints.Session;

public static class SummarySessionEndpoint
{
    public static void MapSummarySessionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/session/{sessionIdentifier}/summary", async (AppDbContext db, Guid sessionIdentifier) =>
        {
            var session = await db.TestSessions
                .Include(s => s.Questions)
                .ThenInclude(q => q.Problem)
                .FirstOrDefaultAsync(s => s.TestSessionIdentifier == sessionIdentifier);

            if (session == null) return Results.NotFound("Session not found");

            return Results.Ok(new
            {
                session.Id,
                session.TestSessionIdentifier,
                session.StartTime,
                session.EndTime,
                session.TotalQuestions,
                session.QuestionsAnswered,
                session.CorrectAnswers,
                session.LongestStreak,
                session.IsActive,
                questions = session.Questions.Select(q => new
                {
                    q.ProblemId,
                    q.Problem.Question,
                    q.Problem.Answer,
                    GivenAnswer = q.GivenAnswer,
                    IsCorrect = q.IsCorrect
                    // GivenAnswer = q.GivenAnswer ?? -1,
                    // IsCorrect = q.IsCorrect ?? false
                })
            });
        });
    }
}
