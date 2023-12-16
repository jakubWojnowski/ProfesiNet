using ProfesiNet.Shared.Events;

namespace ProfesiNet.Users.Application.Events;

internal record UserCreated(Guid Id, string Name, string Surname ) : IEvent;
