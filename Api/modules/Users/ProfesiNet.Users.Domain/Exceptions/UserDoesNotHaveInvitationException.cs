using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class UserDoesNotHaveInvitationException(Guid userId, Guid targetUserId) : ProfesiNetException($"User with id: {userId} does not have invitation from user with id: {targetUserId}");