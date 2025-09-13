using MathTutor.Domain.Entities;
using MathTutor.Application.DTOs;
using MathTutor.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MathTutor.Infrastructure.Data;

namespace MathTutor.Application.Services;

public class SessionService : ISessionService
{
    private readonly AppDbContext _db;

    public SessionService(AppDbContext db) => _db = db;

    public async Task<Guid> StartSessionAsync(int numberOfQuestions)
    {
        var session = new TestSession
        {
            StartedAt = DateTime.UtcNow,
            TotalQuestions = numberOfQuestions
        };
        _db.TestSessions.Add(session);
        await _db.SaveChangesAsync();
        return session.TestSessionIdentifier;
    }

    public async Task<MathProblem> GetNextQuestionAsync(Guid sessionIdentifier)
    {
        var session = await _db.TestSessions
            .FirstOrDefaultAsync(s => s.TestSessionIdentifier == sessionIdentifier && s.IsActive);

        if (session == null)
            throw new InvalidOperationException("No active session found");

        if (session.QuestionsAnswered >= session.TotalQuestions)
        {
            session.IsActive = false;
            session.EndedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return null!;
        }

        var problem = new MathProblem { Question = "5 + 3", Answer = 8 };
        _db.MathProblems.Add(problem);

        var sessionQuestion = new TestSessionQuestion
        {
            TestSessionId = session.Id,
            Problem = problem
        };
        _db.TestSessionQuestions.Add(sessionQuestion);

        await _db.SaveChangesAsync();
        return problem;
    }

    public async Task<SessionAnswerResultDto> SubmitAnswerAsync(Guid sessionIdentifier, long problemId, int answer)
    {
        var session = await _db.TestSessions
            .Include(s => s.Questions)
            .ThenInclude(q => q.Problem)
            .FirstOrDefaultAsync(s => s.TestSessionIdentifier == sessionIdentifier && s.IsActive);

        if (session == null)
            throw new InvalidOperationException("No active session found");

        var sessionQuestion = session.Questions.FirstOrDefault(q => q.Problem.Id == problemId)
            ?? throw new InvalidOperationException("Problem not part of this session");

        bool correct = answer == sessionQuestion.Problem.Answer;
        sessionQuestion.GivenAnswer = answer;
        sessionQuestion.IsCorrect = correct;

        session.QuestionsAnswered++;
        if (correct)
        {
            session.CorrectAnswers++;
            session.CurrentStreak++;
            if (session.CurrentStreak > session.MaxStreak)
                session.MaxStreak = session.CurrentStreak;
        }
        else
        {
            session.CurrentStreak = 0;
        }

        if (session.QuestionsAnswered >= session.TotalQuestions)
        {
            session.IsActive = false;
            session.EndedAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();

        return new SessionAnswerResultDto
        {
            Correct = correct,
            CorrectAnswer = sessionQuestion.Problem.Answer,
            TotalAnswered = session.QuestionsAnswered,
            TotalQuestions = session.TotalQuestions,
            Score = session.CorrectAnswers,
            CurrentStreak = session.CurrentStreak,
            MaxStreak = session.MaxStreak,
            SessionCompleted = !session.IsActive
        };
    }

    public async Task<SessionSummaryDto> GetSessionSummaryAsync(Guid sessionIdentifier)
    {
        var session = await _db.TestSessions
            .Include(s => s.Questions)
            .ThenInclude(q => q.Problem)
            .FirstOrDefaultAsync(s => s.TestSessionIdentifier == sessionIdentifier);

        if (session == null)
            throw new InvalidOperationException("Session not found");

        return new SessionSummaryDto
        {
            SessionId = session.TestSessionIdentifier,
            StartedAt = session.StartedAt,
            EndedAt = session.EndedAt,
            TotalQuestions = session.TotalQuestions,
            QuestionsAnswered = session.QuestionsAnswered,
            CorrectAnswers = session.CorrectAnswers,
            MaxStreak = session.MaxStreak,
            IsActive = session.IsActive,
            Questions = session.Questions.Select(q => new QuestionDto
            {
                ProblemId = q.Problem.Id,
                Question = q.Problem.Question,
                Answer = q.Problem.Answer,
                GivenAnswer = q.GivenAnswer ?? -1,
                IsCorrect = q.IsCorrect ?? false
            }).ToList()
        };
    }
}
