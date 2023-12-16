using Microsoft.Extensions.Logging;
using ProfesiNet.LiveChats.Core.DAL.Repositories;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events.External;

internal class UserNetworkConnectionUpdatedHandler : IEventHandler<UserNetworkConnectionUpdated>
{
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly ILogger<UserNetworkConnectionUpdatedHandler> _logger;

    public UserNetworkConnectionUpdatedHandler(IChatMemberRepository chatMemberRepository, ILogger<UserNetworkConnectionUpdatedHandler> logger)
    {
        _chatMemberRepository = chatMemberRepository;
        _logger = logger;
    }
    public async Task HandleAsync(UserNetworkConnectionUpdated @event)
    {
        var chatMemberSent = await _chatMemberRepository.GetByIdAsync(@event.UserId);
        var chatMemberTarget = await _chatMemberRepository.GetByIdAsync(@event.TargetId);
        if (chatMemberTarget is null)
        {
            _logger.LogError($"Creator with ID: {@event.TargetId} was not found.");
            return;
        }
        if (chatMemberSent is null)
        {
            _logger.LogError($"Creator with ID: {@event.UserId} was not found.");
            return;
        }
        chatMemberSent.NetworkConnections.Add(@event.TargetId);
        chatMemberTarget.NetworkConnections.Add(@event.UserId);
        await _chatMemberRepository.UpdateAsync(chatMemberSent);
        await _chatMemberRepository.UpdateAsync(chatMemberTarget);
        _logger.LogInformation($"Updated a creator with ID: '{chatMemberSent.Id} and creator with Id {chatMemberTarget.Id}'.");
    }
}