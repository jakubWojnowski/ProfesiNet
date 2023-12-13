using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class UserTransactionException(Guid userId) : ProfesiNetException($"User with id {userId} has transaction error");