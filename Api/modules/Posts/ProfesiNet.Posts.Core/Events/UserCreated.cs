using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events;

internal record UserCreated(Guid Id, string Name, string Surname) : IEvent;
