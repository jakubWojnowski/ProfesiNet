using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class UserDosesNotHaveFollowingsException(Guid userId, Guid targetId) : ProfesiNetException(
    $"User with id {userId} dose not have followings with user with id {targetId}");
