using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events;

internal record UserProfilePictureDeleted(Guid UserId) : IEvent;