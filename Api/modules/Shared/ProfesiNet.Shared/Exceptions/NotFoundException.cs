namespace ProfesiNet.Shared.Exceptions;

public sealed class NotFoundException(string message) : Exception(message);