using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events;

internal record UserFollowingsDeleted(Guid UserId,Guid TargetId) : IEvent;