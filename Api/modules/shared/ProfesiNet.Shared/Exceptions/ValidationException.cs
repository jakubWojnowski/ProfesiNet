namespace ProfesiNet.Shared.Exceptions;

public class ValidationException(string errors) : Exception("One or more validation failures have occurred.")
{
    public string Errors { get; } = errors;
}