using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class UserDosesNotHaveConnectionException(Guid userId, Guid targetId) : ProfesiNetException(
    $"User with id {userId} dose not have connection with user with id {targetId}");

