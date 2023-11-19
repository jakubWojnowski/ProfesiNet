namespace ProfesiNet.Users.Domain.Exceptions;

public sealed class UserAlreadyExistsException(string message) : Exception(message);