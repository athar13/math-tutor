using MathTutor.Api.Data;
using MathTutor.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MathTutor.Api.Endpoints.Session;

public static class AnswerSessionEndpoint
{
    public static void MapAnswerSessionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/session/{sessionIdentifier}/answer", async (AppDbContext db, Guid sessionIdentifier, AnswerSubmission submission) =>
        {
            var session = await db.TestSessions
                .Include(s => s.Questions)
                .FirstOrDefaultAsync(s => s.TestSessionIdentifier == sessionIdentifier && s.IsActive);

            if (session == null) return Results.NotFound("No active session");

            var sessionQuestion = session.Questions.FirstOrDefault(q => q.ProblemId == submission.Id);
            if (sessionQuestion == null) return Results.NotFound("Question not part of this session");

            var problem = await db.MathProblems.FindAsync(submission.Id);
            if (problem == null) return Results.NotFound("Problem not found");

            bool correct = submission.Answer == problem.Answer;

            sessionQuestion.GivenAnswer = submission.Answer;
            sessionQuestion.IsCorrect = correct;

            session.QuestionsAnswered++;
            if (correct)
            {
                session.CorrectAnswers++;
                session.CurrentStreak++;
                if (session.CurrentStreak > session.LongestStreak)
                    session.LongestStreak = session.CurrentStreak;
            }
            else
            {
                session.CurrentStreak = 0;
            }

            if (session.QuestionsAnswered >= session.TotalQuestions)
            {
                session.IsActive = false;
                session.EndTime = DateTime.UtcNow;
            }

            await db.SaveChangesAsync();

            return Results.Ok(new
            {
                correct,
                correctAnswer = problem.Answer,
                totalAnswered = session.QuestionsAnswered,
                totalQuestions = session.TotalQuestions,
                score = session.CorrectAnswers,
                currentStreak = session.CurrentStreak,
                maxStreak = session.LongestStreak,
                sessionCompleted = !session.IsActive
            });
        });
    }
}
