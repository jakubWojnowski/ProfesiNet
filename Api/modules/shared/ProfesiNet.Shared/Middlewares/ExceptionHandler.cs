using System.Security.Authentication;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Shared.Middlewares;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var errorResponse = exception switch
        {
            NotFoundException => CreateErrorResponse(StatusCodes.Status404NotFound, exception.Message),
            BadRequestException => CreateErrorResponse(StatusCodes.Status400BadRequest, exception.Message),
            TokenNotFoundException => CreateErrorResponse(StatusCodes.Status401Unauthorized, exception.Message),
            AuthenticationException => CreateErrorResponse(StatusCodes.Status403Forbidden, "Authentication failed."),
            AccessDeniedException => CreateErrorResponse(StatusCodes.Status403Forbidden, "Access denied."),
            ValidationException ex => CreateErrorResponse(StatusCodes.Status422UnprocessableEntity, ex.Errors),
            ApiException ex => CreateErrorResponse(ex.StatusCode, ex.Message),
            DbUpdateException => CreateErrorResponse(StatusCodes.Status409Conflict, "Database update exception occurred."),
            KeyNotFoundException => CreateErrorResponse(StatusCodes.Status404NotFound, "Specified key was not found."),
            NotImplementedException => CreateErrorResponse(StatusCodes.Status501NotImplemented, "Method not implemented."),
            TimeoutException => CreateErrorResponse(StatusCodes.Status408RequestTimeout, "Request timed out."),
            UnauthorizedAccessException => CreateErrorResponse(StatusCodes.Status401Unauthorized, "Unauthorized access."),
            FormatException => CreateErrorResponse(StatusCodes.Status400BadRequest, "Format exception."),
            InvalidOperationException => CreateErrorResponse(StatusCodes.Status400BadRequest, "Invalid operation."),
            _ => CreateErrorResponse(StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };

        httpContext.Response.StatusCode = errorResponse.Code;
        httpContext.Response.ContentType = "application/json";

        var errorJson = JsonSerializer.Serialize(new { errorResponse.Message, errorResponse.Details });
        await httpContext.Response.WriteAsync(errorJson, cancellationToken);

        _logger.LogError(exception, exception.Message);

        return true;
    }

    private (int Code, string Message, object Details) CreateErrorResponse(int statusCode, string message, object details = null)
    {
        return (statusCode, message, details ?? new object());
    }

}