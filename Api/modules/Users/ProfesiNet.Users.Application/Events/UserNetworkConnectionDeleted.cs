using ProfesiNet.Shared.Events;

namespace ProfesiNet.Users.Application.Events;

internal record UserNetworkConnectionDeleted(Guid UserId, Guid TargetId) : IEvent;