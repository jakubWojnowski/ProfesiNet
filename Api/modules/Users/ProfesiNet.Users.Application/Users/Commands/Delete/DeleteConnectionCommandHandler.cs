using MediatR;
using ProfesiNet.Shared.Messaging;
using ProfesiNet.Users.Application.Events;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal class DeleteConnectionCommandHandler : IRequestHandler<DeleteConnectionCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageBroker _messageBroker;

    public DeleteConnectionCommandHandler(IUserRepository userRepository, IMessageBroker messageBroker)
    {
        _userRepository = userRepository;
        _messageBroker = messageBroker;
    }

    public async Task Handle(DeleteConnectionCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteConnectionAsync(request.UserId, request.TargetId, cancellationToken);
        await _messageBroker.PublishMessageAsync(new UserNetworkConnectionDeleted(request.UserId, request.TargetId));
    }
}