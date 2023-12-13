using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class UserAlreadyFollowedException(Guid userId, Guid targetId) : ProfesiNetException($"User with id {userId} already followed user with id {targetId}");
