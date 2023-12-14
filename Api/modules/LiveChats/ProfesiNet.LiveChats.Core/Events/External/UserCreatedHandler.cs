using Microsoft.Extensions.Logging;
using ProfesiNet.LiveChats.Core.DAL.Entities;
using ProfesiNet.LiveChats.Core.DAL.Repositories;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.LiveChats.Core.Events.External;

internal class UserCreatedHandler : IEventHandler<UserCreated>
{
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly ILogger<UserCreatedHandler> _logger;

    public UserCreatedHandler(IChatMemberRepository chatMemberRepository, ILogger<UserCreatedHandler> logger)
    {
        _chatMemberRepository = chatMemberRepository;
        _logger = logger;
    }

    public async Task HandleAsync(UserCreated @event)
    {
        var chatMember = new ChatMember()
        {
            Id = @event.Id,
            Name = @event.Name,
            Surname = @event.Surname
            
        };
        await _chatMemberRepository.AddAsync(chatMember);
        _logger.LogInformation($"Added a creator with ID: '{chatMember.Id}'.");
    }
}