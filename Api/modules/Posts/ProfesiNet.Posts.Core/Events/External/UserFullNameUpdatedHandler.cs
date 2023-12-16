using Microsoft.Extensions.Logging;
using ProfesiNet.Posts.Core.DAL.Entities;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events.External;

internal class UserFullNameUpdatedHandler : IEventHandler<UserFullNameUpdated>
{
    private readonly ICreatorRepository _creatorRepository;
    private readonly ILogger<UserFullNameUpdatedHandler> _logger;

    public UserFullNameUpdatedHandler(ICreatorRepository creatorRepository, ILogger<UserFullNameUpdatedHandler> logger)
    {
        _creatorRepository = creatorRepository;
        _logger = logger;
    }


    public async Task HandleAsync(UserFullNameUpdated @event)
    {
        var creator = await _creatorRepository.GetByIdAsync(@event.UserId);
        if (creator is null)
        {
            _logger.LogError($"Creator with ID: {@event.UserId} was not found.");
            return;
        }

        creator.Name = @event.Name ?? creator.Name;
        creator.Surname = @event.Surname ?? creator.Surname;
        await _creatorRepository.UpdateAsync(creator);
        _logger.LogInformation($"Updated a creator with ID: '{creator.Id}'.");
    }
}