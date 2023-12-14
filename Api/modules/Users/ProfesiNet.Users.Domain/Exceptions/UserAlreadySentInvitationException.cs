using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class UserAlreadySentInvitationException(Guid userId, Guid targetId) : ProfesiNetException(
    $"User with id {userId} already sent invitation to user with id {targetId}");
