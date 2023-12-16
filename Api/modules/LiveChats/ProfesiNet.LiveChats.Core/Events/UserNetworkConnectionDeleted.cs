using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events;

internal record UserNetworkConnectionDeleted(Guid UserId, Guid TargetId) : IEvent;