using Microsoft.Extensions.Primitives;

namespace Web.Api.Middleware;

public class RequestContextLoggingMiddleware(RequestDelegate next)
{
    private const string CorrelationIdHeaderName = "Correlation-Id";

    //TODO: add Serilog later
    // public Task Invoke(HttpContext context)
    // {
    //     using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
    //     {
    //         return next.Invoke(context);
    //     }
    // }

    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(
            CorrelationIdHeaderName,
            out StringValues correlationId);

        return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
    }
}