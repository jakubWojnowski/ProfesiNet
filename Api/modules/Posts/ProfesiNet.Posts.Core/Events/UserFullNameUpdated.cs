using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events;

internal record UserFullNameUpdated(Guid UserId,string? Name, string? Surname) : IEvent;
