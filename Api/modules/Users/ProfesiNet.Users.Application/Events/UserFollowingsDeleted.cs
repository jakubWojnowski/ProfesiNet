using ProfesiNet.Shared.Events;

namespace ProfesiNet.Users.Application.Events;

internal record UserFollowingsDeleted(Guid UserId,Guid TargetId) : IEvent;