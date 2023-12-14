using ProfesiNet.Shared.Events;

namespace ProfesiNet.Users.Application.Events;

public record UserFollowingsUpdated(Guid UserId,Guid TargetId) : IEvent;