using Microsoft.Extensions.Logging;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events.External;

internal class UserProfilePictureAddedHandler : IEventHandler<UserProfilePictureAdded>
{
    private readonly ICreatorRepository _creatorRepository;
    private readonly ILogger<UserProfilePictureAddedHandler> _logger;

    public UserProfilePictureAddedHandler(ICreatorRepository creatorRepository, ILogger<UserProfilePictureAddedHandler> logger)
    {
        _creatorRepository = creatorRepository;
        _logger = logger;
    }
    
    public async Task HandleAsync(UserProfilePictureAdded @event)
    {
        var chatMember = await _creatorRepository.GetByIdAsync(@event.UserId);
        if (chatMember is null)
        {
            _logger.LogError($"Creator with ID: {@event.UserId} was not found.");
            return;
        }
        chatMember.ProfilePicture = @event.Url;
        await _creatorRepository.UpdateAsync(chatMember);
        _logger.LogInformation($"Updated a creator with ID: '{chatMember.Id}'.");
    }
}