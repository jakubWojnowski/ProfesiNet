using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events;

internal record UserProfilePictureAdded(Guid UserId, string Url) : IEvent;