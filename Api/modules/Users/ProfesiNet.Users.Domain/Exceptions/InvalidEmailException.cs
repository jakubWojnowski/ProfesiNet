using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class InvalidEmailException(string message) : ProfesiNetException($"{message} invalid");