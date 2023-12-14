using MediatR;
using ProfesiNet.Shared.Messaging;
using ProfesiNet.Users.Application.Events;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal class UpdateUserFollowingsCommandHandler : IRequestHandler<UpdateUserFollowingsCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageBroker _messageBroker;

    public UpdateUserFollowingsCommandHandler(IUserRepository userRepository, IMessageBroker messageBroker)
    {
        _userRepository = userRepository;
        _messageBroker = messageBroker;
    }

    public async Task Handle(UpdateUserFollowingsCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.UpdateFollowingsAsync(request.UserId, request.TargetId, cancellationToken);
        await _messageBroker.PublishAsync(new UserFollowingsUpdated(request.UserId,request.TargetId));
    }
        
}