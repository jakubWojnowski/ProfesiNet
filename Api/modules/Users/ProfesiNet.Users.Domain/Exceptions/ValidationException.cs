namespace ProfesiNet.Users.Domain.Exceptions;

public sealed class ValidationException(string message) : Exception(message);