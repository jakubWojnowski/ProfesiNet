using Microsoft.Extensions.Logging;
using ProfesiNet.LiveChats.Core.DAL.Repositories;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events.External;

internal class UserDeletedHandler : IEventHandler<UserDeleted>
{
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly ILogger<UserCreatedHandler> _logger;


    public UserDeletedHandler(IChatMemberRepository chatMemberRepository, ILogger<UserCreatedHandler> logger)
    {
        _chatMemberRepository = chatMemberRepository;
        _logger = logger;
    }
    public async Task HandleAsync(UserDeleted @event)
    {
        var chatMember = await _chatMemberRepository.GetByIdAsync(@event.UserId);
        await _chatMemberRepository.DeleteAsync(chatMember);
    }
}