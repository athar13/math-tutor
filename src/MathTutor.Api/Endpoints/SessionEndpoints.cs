namespace MathTutor.Api.Endpoints;

public static class SessionEndpoints
{
    public static void MapSessionEndpoints(this IEndpointRouteBuilder app)
    {
        Session.StartSessionEndpoint.MapStartSessionEndpoint(app);
        Session.NextSessionEndpoint.MapNextSessionEndpoint(app);
        Session.AnswerSessionEndpoint.MapAnswerSessionEndpoint(app);
        Session.SummarySessionEndpoint.MapSummarySessionEndpoint(app);
    }
}

