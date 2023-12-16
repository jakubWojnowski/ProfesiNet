using Microsoft.Extensions.Logging;
using ProfesiNet.LiveChats.Core.DAL.Repositories;
using ProfesiNet.Posts.Core.Events;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events.External;

internal class UserProfilePictureDeletedHandler : IEventHandler<UserProfilePictureDeleted>
{
    private readonly IChatMemberRepository _chatMemberRepository;

    private readonly ILogger<UserProfilePictureDeletedHandler> _logger;

    public UserProfilePictureDeletedHandler(IChatMemberRepository chatMemberRepository, ILogger<UserProfilePictureDeletedHandler> logger)
    {
        _chatMemberRepository = chatMemberRepository;
        _logger = logger;
    }
    public async Task HandleAsync(UserProfilePictureDeleted @event)
    {
        var chatMember = await _chatMemberRepository.GetByIdAsync(@event.UserId);
        if (chatMember is null)
        {
            _logger.LogError($"Creator with ID: {@event.UserId} was not found.");
            return;
        }
        chatMember.ProfilePicture = null;
        await _chatMemberRepository.UpdateAsync(chatMember);
        _logger.LogInformation($"Updated a creator with ID: '{chatMember.Id}'.");
        
        
    }
}