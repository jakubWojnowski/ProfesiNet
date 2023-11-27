using Microsoft.AspNetCore.Http;

namespace ProfesiNet.Shared.Exceptions;

public class ApiException(string message, int statusCode = StatusCodes.Status500InternalServerError)
    : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}