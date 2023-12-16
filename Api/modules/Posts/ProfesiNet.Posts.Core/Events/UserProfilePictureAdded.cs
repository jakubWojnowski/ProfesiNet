using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events;

internal record UserProfilePictureAdded(Guid UserId, string Url) : IEvent;