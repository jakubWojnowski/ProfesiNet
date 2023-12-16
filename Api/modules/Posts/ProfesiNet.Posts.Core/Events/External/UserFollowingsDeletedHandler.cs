using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events.External;

internal class UserFollowingsDeletedHandler : IEventHandler<UserFollowingsDeleted>
{
    private readonly ICreatorRepository _creatorRepository;

    public UserFollowingsDeletedHandler(ICreatorRepository creatorRepository)
    {
        _creatorRepository = creatorRepository;
    }
    public async Task HandleAsync(UserFollowingsDeleted @event)
    {
        var creator = await _creatorRepository.GetByIdAsync(@event.UserId);
        if (creator is null)
        {
            return;
        }

        if (creator.Followings.Contains(@event.TargetId))
        {
            creator.Followings.Remove(@event.TargetId);
        }
        await _creatorRepository.UpdateAsync(creator);
    }
}