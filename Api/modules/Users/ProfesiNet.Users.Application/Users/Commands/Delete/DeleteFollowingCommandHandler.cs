using MediatR;
using ProfesiNet.Shared.Messaging;
using ProfesiNet.Users.Application.Events;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal class DeleteFollowingCommandHandler : IRequestHandler<DeleteFollowingCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageBroker _messageBroker;

    public DeleteFollowingCommandHandler(IUserRepository userRepository, IMessageBroker messageBroker)
    {
        _userRepository = userRepository;
        _messageBroker = messageBroker;
    }

    public async Task Handle(DeleteFollowingCommand request, CancellationToken cancellationToken)
    { 
        await _userRepository.DeleteFollowingAsync(request.UserId, request.TargetId, cancellationToken);
        await _messageBroker.PublishMessageAsync(new UserFollowingsDeleted(request.UserId,request.TargetId));
    }
       
}