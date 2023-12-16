using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events;

internal record UserNetworkConnectionUpdated(Guid UserId, Guid TargetId) : IEvent;