using Microsoft.Extensions.Logging;
using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events.External;

internal class UserCreatedHandler : IEventHandler<UserCreated>
{
    private readonly ICreatorRepository _creatorRepository;
    private readonly ILogger<UserCreatedHandler> _logger;

    public UserCreatedHandler(ICreatorRepository creatorRepository, ILogger<UserCreatedHandler> logger)
    {
        _creatorRepository = creatorRepository;
        _logger = logger;
    }

    public async Task HandleAsync(UserCreated @event)
    {
        var creator = new Creator
        {
            Id = @event.Id,
            Name = @event.Name,
            Surname = @event.Surname
        };
        await _creatorRepository.AddAsync(creator);
        _logger.LogInformation($"Added a creator with ID: '{creator.Id}'.");
    }
}