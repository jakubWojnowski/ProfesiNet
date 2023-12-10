using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events;

public record UserCreated(Guid Id, string Name, string Surname) : IEvent;
