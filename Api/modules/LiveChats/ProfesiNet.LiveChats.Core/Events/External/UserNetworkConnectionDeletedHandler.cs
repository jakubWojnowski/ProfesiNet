using Microsoft.Extensions.Logging;
using ProfesiNet.LiveChats.Core.DAL.Repositories;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events.External;

internal class UserNetworkConnectionDeletedHandler :  IEventHandler<UserNetworkConnectionDeleted>
{
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly ILogger<UserNetworkConnectionDeletedHandler> _logger;

    public UserNetworkConnectionDeletedHandler(IChatMemberRepository chatMemberRepository, ILogger<UserNetworkConnectionDeletedHandler> logger)
    {
        _chatMemberRepository = chatMemberRepository;
        _logger = logger;
    }
    public async Task HandleAsync(UserNetworkConnectionDeleted @event)
    {
        var chatMember = await _chatMemberRepository.GetByIdAsync(@event.UserId);
        var chatMemberTarget = await _chatMemberRepository.GetByIdAsync(@event.TargetId);
        if (chatMemberTarget is null)
        {
            _logger.LogError($"Creator with ID: {@event.TargetId} was not found.");
            return;
        }
        if (chatMember is null)
        {
            _logger.LogError($"Creator with ID: {@event.UserId} was not found.");
            return;
        }
        chatMember.NetworkConnections.Remove(@event.TargetId);
        chatMemberTarget.NetworkConnections.Remove(@event.UserId);
        await _chatMemberRepository.UpdateAsync(chatMember);
        await _chatMemberRepository.UpdateAsync(chatMemberTarget);
        _logger.LogInformation($"Updated a creator with ID: '{chatMember.Id}'.");
    }
}