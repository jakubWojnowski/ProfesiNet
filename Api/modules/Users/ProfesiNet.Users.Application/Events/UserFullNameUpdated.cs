using ProfesiNet.Shared.Events;

namespace ProfesiNet.Users.Application.Events;

internal record UserFullNameUpdated(Guid UserId,string? Name, string? Surname) : IEvent;
