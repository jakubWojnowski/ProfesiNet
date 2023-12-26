using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class InvalidEmailException(string email) : ProfesiNetException($"{email} is an invalid email address");