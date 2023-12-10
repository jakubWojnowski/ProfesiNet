using ProfesiNet.Shared.Events;

namespace ProfesiNet.Users.Application.Events;

public record UserCreated(Guid Id, string Name, string Surname) : IEvent;
