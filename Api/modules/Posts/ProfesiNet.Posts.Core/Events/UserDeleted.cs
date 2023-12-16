using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events;

internal record UserDeleted(Guid UserId) : IEvent;