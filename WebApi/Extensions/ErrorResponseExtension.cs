using System.Net;
using System.Net.Mime;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Middlewares;

namespace WebApi.Extensions;

internal static class ErrorResponseExtension
{
    private const string ErrorMessage = "something_went_wrong";

    internal static async Task GenerateApplicationErrorResponse(this ApplicationException httpResponseException,
        HttpContext httpContext)
    {
        ApplicationExceptionBase exception = httpResponseException as ApplicationExceptionBase;
        httpContext.Response.StatusCode = exception!.StatusCode;

        ProblemDetails error = new() { Title = httpResponseException.Message };

        await httpContext.Response.WriteAsJsonAsync(error);
    }

    internal static async Task GenerateUnhandledErrorResponse(this Exception exception,
        HttpContext httpContext, ILogger<ErrorHandlerMiddleware> logger)
    {
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        
        ProblemDetails errorResponse = new() { Title = ErrorMessage };

        logger.LogError(exception.ToFullBlownString()); // Add user info here

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(errorResponse);
    }
}