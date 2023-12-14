using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class UserAlreadyAcceptedConnectionException(Guid userId, Guid targetUserId) : ProfesiNetException($"User with id: {userId} already accepted connection invitation from user with id: {targetUserId}");