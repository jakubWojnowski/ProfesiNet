using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events;

public record UserFullNameUpdated(Guid UserId,string? Name, string? Surname) : IEvent;
