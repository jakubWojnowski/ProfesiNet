using Microsoft.Extensions.Logging;
using ProfesiNet.LiveChats.Core.DAL.Repositories;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events.External;

internal class UserFullNameUpdatedHandler : IEventHandler<UserFullNameUpdated>
{
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly ILogger<UserFullNameUpdatedHandler> _logger;

    public UserFullNameUpdatedHandler(IChatMemberRepository chatMemberRepository, ILogger<UserFullNameUpdatedHandler> logger)
    {
        _chatMemberRepository = chatMemberRepository;
        _logger = logger;
    }


    public async Task HandleAsync(UserFullNameUpdated @event)
    {
        var chatMember = await _chatMemberRepository.GetByIdAsync(@event.UserId);
        if (chatMember is null)
        {
            _logger.LogError($"Creator with ID: {@event.UserId} was not found.");
            return;
        }

        chatMember.Name = @event.Name ?? chatMember.Name;
        chatMember.Surname = @event.Surname ?? chatMember.Surname;
        await _chatMemberRepository.UpdateAsync(chatMember);
        _logger.LogInformation($"Updated a creator with ID: '{chatMember.Id}'.");
    }
}