using Microsoft.Extensions.Logging;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events.External;

internal class UserFollowingsUpdatedHandler : IEventHandler<UserFollowingsUpdated>
{
    private readonly ICreatorRepository _creatorRepository;
    private readonly ILogger<UserFollowingsUpdatedHandler> _logger;

    public UserFollowingsUpdatedHandler(ICreatorRepository creatorRepository,
        ILogger<UserFollowingsUpdatedHandler> logger)
    {
        _creatorRepository = creatorRepository;
        _logger = logger;
    }

    public async Task HandleAsync(UserFollowingsUpdated @event)
    {
        var creator = await _creatorRepository.GetByIdAsync(@event.UserId);
        if (creator is null)
        {
            _logger.LogError($"Creator with ID: {@event.UserId} was not found.");
            return;
        }

        if (creator.Followings.Contains(@event.TargetId))
        {
            creator.Followings.Remove(@event.TargetId);
            _logger.LogInformation($"Updated a creator with ID: '{creator.Id}'.");
        }
        creator.Followings.Add(@event.TargetId);
        await _creatorRepository.UpdateAsync(creator);
    }
}