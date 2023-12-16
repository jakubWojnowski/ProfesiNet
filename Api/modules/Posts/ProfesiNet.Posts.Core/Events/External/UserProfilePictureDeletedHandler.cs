using Microsoft.Extensions.Logging;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events.External;

internal class UserProfilePictureDeletedHandler : IEventHandler<UserProfilePictureDeleted>
{
    private readonly ICreatorRepository _creatorRepository;
    private readonly ILogger<UserProfilePictureDeletedHandler> _logger;

    public UserProfilePictureDeletedHandler(ICreatorRepository creatorRepository, ILogger<UserProfilePictureDeletedHandler> logger)
    {
        _creatorRepository = creatorRepository;
        _logger = logger;
    }
    public async Task HandleAsync(UserProfilePictureDeleted @event)
    {
        var chatMember = await _creatorRepository.GetByIdAsync(@event.UserId);
        if (chatMember is null)
        {
            _logger.LogError($"Creator with ID: {@event.UserId} was not found.");
            return;
        }
        chatMember.ProfilePicture = null;
        await _creatorRepository.UpdateAsync(chatMember);
        _logger.LogInformation($"Updated a creator with ID: '{chatMember.Id}'.");
        
        
    }
}