using ProfesiNet.Shared.Events;

namespace ProfesiNet.Users.Application.Events;

public record UserFollowingsDeleted(Guid UserId,Guid TargetId) : IEvent;