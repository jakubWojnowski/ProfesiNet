using ProfesiNet.Shared.Events;

namespace ProfesiNet.Users.Application.Events;

internal record UserNetworkConnectionUpdated(Guid UserId, Guid TargetId) : IEvent;