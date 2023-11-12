namespace WebApi.Middlewares;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Check if the request already contains a correlation ID
        var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault();

        // If not, generate a new one
        if (string.IsNullOrEmpty(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
            context.Request.Headers.Add("X-Correlation-ID", correlationId);
        }

        // Set the correlation ID in the response headers for client-side tracking
        context.Response.Headers.Add("X-Correlation-ID", correlationId);

        // Set the correlation ID in the request context for downstream components
        context.Items["CorrelationId"] = correlationId;

        // Call the next middleware in the pipeline
        await _next(context);
    }
}