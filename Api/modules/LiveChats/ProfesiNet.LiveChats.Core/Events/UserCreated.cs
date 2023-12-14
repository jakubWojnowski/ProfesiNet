using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events;

internal record UserCreated(Guid Id, string Name, string Surname) : IEvent;
