using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events;

internal record UserFollowingsUpdated(Guid UserId, Guid TargetId) : IEvent;