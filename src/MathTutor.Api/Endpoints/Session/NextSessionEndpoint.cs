using MathTutor.Api.Data;
using MathTutor.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MathTutor.Api.Endpoints.Session;

public static class NextSessionEndpoint
{
    public static void MapNextSessionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/session/{sessionIdentifier}/next", async (AppDbContext db, Guid sessionIdentifier) =>
        {
            var session = await db.TestSessions
                .FirstOrDefaultAsync(s => s.TestSessionIdentifier == sessionIdentifier && s.IsActive);

            if (session == null) return Results.NotFound("No active session");

            if (session.QuestionsAnswered >= session.TotalQuestions)
            {
                session.IsActive = false;
                session.EndTime = DateTime.UtcNow;
                await db.SaveChangesAsync();
                return Results.Ok(new { message = "Session completed" });
            }

            var problem = MathProblem.Generate();
            db.MathProblems.Add(problem);

            var sessionQuestion = new TestSessionQuestion
            {
                TestSessionId = session.Id,
                Problem = problem
            };
            db.TestSessionQuestions.Add(sessionQuestion);

            await db.SaveChangesAsync();

            return Results.Ok(problem);
        });
    }
}
