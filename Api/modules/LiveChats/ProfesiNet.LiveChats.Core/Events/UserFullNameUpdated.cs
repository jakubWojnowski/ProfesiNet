using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events;

internal record UserFullNameUpdated(Guid UserId,string? Name, string? Surname) : IEvent;
