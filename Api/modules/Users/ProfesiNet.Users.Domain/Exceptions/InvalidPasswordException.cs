using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class InvalidPasswordException() : ProfesiNetException("Invalid password");