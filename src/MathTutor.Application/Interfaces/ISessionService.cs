using MathTutor.Domain.Entities;
using MathTutor.Application.DTOs;

namespace MathTutor.Application.Interfaces;

public interface ISessionService
{
    Task<Guid> StartSessionAsync(int numberOfQuestions);
    Task<MathProblem> GetNextQuestionAsync(Guid sessionIdentifier);
    Task<SessionAnswerResultDto> SubmitAnswerAsync(Guid sessionIdentifier, long problemId, int answer);
    Task<SessionSummaryDto> GetSessionSummaryAsync(Guid sessionIdentifier);
}
