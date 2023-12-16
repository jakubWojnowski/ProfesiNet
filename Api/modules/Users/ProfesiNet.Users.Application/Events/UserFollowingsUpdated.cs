using ProfesiNet.Shared.Events;

namespace ProfesiNet.Users.Application.Events;

internal record UserFollowingsUpdated(Guid UserId,Guid TargetId) : IEvent;