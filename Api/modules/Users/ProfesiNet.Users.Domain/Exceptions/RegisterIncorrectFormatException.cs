using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class RegisterIncorrectFormatException(string message) : ProfesiNetValidationException(message);
