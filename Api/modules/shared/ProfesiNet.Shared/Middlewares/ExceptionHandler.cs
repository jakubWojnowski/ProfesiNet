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
        var (statusCodes, errorMessage) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            BadRequestException => (StatusCodes.Status400BadRequest, exception.Message),
            TokenNotFoundException => (StatusCodes.Status401Unauthorized, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error")
        };
        
        httpContext.Response.StatusCode = statusCodes;
        httpContext.Response.ContentType = "application/json";
        
        await httpContext.Response.WriteAsync(errorMessage, cancellationToken);
        
        _logger.LogError(exception, exception.Message);
        
        return true;
    }
}