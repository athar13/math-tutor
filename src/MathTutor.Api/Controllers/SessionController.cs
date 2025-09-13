using Microsoft.AspNetCore.Mvc;
using MathTutor.Application.DTOs;
using MathTutor.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService) => _sessionService = sessionService;

    [HttpPost("start")]
    public async Task<IActionResult> Start([FromBody] SessionConfigDto config)
    {
        var sessionId = await _sessionService.StartSessionAsync(config.NumberOfQuestions);
        return Ok(new { sessionId, message = "Session started" });
    }

    [HttpGet("{sessionId:guid}/next")]
    public async Task<IActionResult> Next(Guid sessionId)
    {
        var problem = await _sessionService.GetNextQuestionAsync(sessionId);
        if (problem == null)
            return Ok(new { message = "Session completed" });

        return Ok(new { problem.Id, problem.Question });
    }

    [HttpPost("{sessionId:guid}/answer")]
    public async Task<IActionResult> SubmitAnswer(Guid sessionId, [FromBody] AnswerSubmissionDto submission)
    {
        var result = await _sessionService.SubmitAnswerAsync(sessionId, submission.Id, submission.Answer);
        return Ok(result);
    }

    [HttpGet("{sessionId:guid}/summary")]
    public async Task<IActionResult> Summary(Guid sessionId)
    {
        var summary = await _sessionService.GetSessionSummaryAsync(sessionId);
        return Ok(summary);
    }
}
