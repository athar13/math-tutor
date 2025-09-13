using MathTutor.Api.Data;
using MathTutor.Api.Models;

namespace MathTutor.Api.Endpoints.Session;

public static class StartSessionEndpoint
{
    public static void MapStartSessionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/session/start", async (AppDbContext db, SessionConfig config) =>
        {
            var testSession = new TestSession
            {
                StartTime = DateTime.UtcNow,
                TotalQuestions = config.NumberOfQuestions,
                QuestionsAnswered = 0,
                CorrectAnswers = 0,
                IsActive = true,
                CurrentStreak = 0,
                LongestStreak = 0
            };

            db.TestSessions.Add(testSession);
            await db.SaveChangesAsync();

            return Results.Ok(new
            {
                sessionId = testSession.TestSessionIdentifier,
                createdAt = testSession.StartTime,
                message = "Session started"
            });
        });
    }
}