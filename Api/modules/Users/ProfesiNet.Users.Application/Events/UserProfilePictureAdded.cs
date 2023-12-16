using ProfesiNet.Shared.Events;

namespace ProfesiNet.Users.Application.Events;

internal record UserProfilePictureAdded(Guid UserId, string Url) : IEvent;