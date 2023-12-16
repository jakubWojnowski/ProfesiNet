using ProfesiNet.Shared.Events;

namespace ProfesiNet.Users.Application.Events;

internal record UserProfilePictureDeleted(Guid UserId) : IEvent;