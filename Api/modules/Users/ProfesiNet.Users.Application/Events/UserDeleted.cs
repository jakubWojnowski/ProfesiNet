using ProfesiNet.Shared.Events;

namespace ProfesiNet.Users.Application.Events;

internal record UserDeleted(Guid UserId) : IEvent;