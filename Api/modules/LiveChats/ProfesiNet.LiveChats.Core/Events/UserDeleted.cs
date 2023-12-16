using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events;

internal record UserDeleted(Guid UserId) : IEvent;