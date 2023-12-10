using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Shared.Events;

namespace ProfesiNet.Posts.Core.Events.External;

internal class CreatorDeletedHandler : IEventHandler<UserDeleted>
{
    private readonly ICreatorRepository _creatorRepository;

    public CreatorDeletedHandler(ICreatorRepository creatorRepository)
    {
        _creatorRepository = creatorRepository;
    }
    public async Task HandleAsync(UserDeleted @event)
    {
        var creator = await _creatorRepository.GetByIdAsync(@event.UserId);
        if (creator != null) await _creatorRepository.DeleteAsync(creator);
    }
}