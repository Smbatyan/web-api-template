using System.Text;
using WebApi.Extensions;

namespace WebApi.Middlewares;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await LogRequest(context);
            var originalResponseBody = context.Response.Body;

            using MemoryStream responseBody = new();
            context.Response.Body = responseBody;
            await _next.Invoke(context);

            await LogResponse(context, responseBody, originalResponseBody);
        }
        catch (Exception ex)
        {
            await ex.GenerateUnhandledErrorResponse(context, _logger);
        }
    }

    private async Task LogResponse(HttpContext context, MemoryStream responseBody, Stream originalResponseBody)
    {
        responseBody.Position = 0;
        var content = await new StreamReader(responseBody).ReadToEndAsync();
        responseBody.Position = 0;
        await responseBody.CopyToAsync(originalResponseBody);

        _logger.LogInformation(BuildResponseInfo(context, content).ToString());

        context.Response.Body = originalResponseBody;
    }

    private StringBuilder BuildResponseInfo(HttpContext context, string content)
    {
        var responseContent = new StringBuilder();
        responseContent.AppendLine("=== Response Info ===");
        responseContent.AppendLine("-- headers --");

        foreach (var (headerKey, headerValue) in context.Response.Headers)
        {
            responseContent.Append($"header = {headerKey}    value = {headerValue}\n");
        }

        responseContent.AppendLine("-- body --");
        responseContent.Append($"body = {content}\n");

        return responseContent;
    }

    private async Task LogRequest(HttpContext context)
    {
        context.Request.EnableBuffering();
        var requestReader = new StreamReader(context.Request.Body);
        var content = await requestReader.ReadToEndAsync();

        _logger.LogInformation(BuildRequestInfo(context, content).ToString());

        context.Request.Body.Position = 0;
    }

    private StringBuilder BuildRequestInfo(HttpContext context, string content)
    {
        var requestContent = new StringBuilder();
        requestContent.AppendLine("=== Request Info ===");
        requestContent.AppendLine($"[{context.Request.Method.ToUpper()}] - {context.Request.Path}");
        requestContent.AppendLine("-- headers --");

        foreach (var (headerKey, headerValue) in context.Request.Headers)
        {
            requestContent.Append($"header = {headerKey}    value = {headerValue}\n");
        }

        requestContent.AppendLine("-- body --");
        requestContent.Append($"body = {content}\n");

        return requestContent;
    }
}