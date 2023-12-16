using Microsoft.Extensions.Logging;
using ProfesiNet.LiveChats.Core.DAL.Repositories;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events.External;

internal class UserProfilePictureAddedHandler : IEventHandler<UserProfilePictureAdded>
{
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly ILogger<UserProfilePictureAddedHandler> _logger;

    public UserProfilePictureAddedHandler(IChatMemberRepository chatMemberRepository, ILogger<UserProfilePictureAddedHandler> logger)
    {
        _chatMemberRepository = chatMemberRepository;
        _logger = logger;
    }
    
    public async Task HandleAsync(UserProfilePictureAdded @event)
    {
        var chatMember = await _chatMemberRepository.GetByIdAsync(@event.UserId);
        if (chatMember is null)
        {
            _logger.LogError($"Creator with ID: {@event.UserId} was not found.");
            return;
        }
        chatMember.ProfilePicture = @event.Url;
        await _chatMemberRepository.UpdateAsync(chatMember);
        _logger.LogInformation($"Updated a creator with ID: '{chatMember.Id}'.");
    }
}