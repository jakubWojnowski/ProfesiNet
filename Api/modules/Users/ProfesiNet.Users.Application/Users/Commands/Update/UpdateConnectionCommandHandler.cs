using MediatR;
using ProfesiNet.Shared.Messaging;
using ProfesiNet.Users.Application.Events;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal class UpdateConnectionCommandHandler : IRequestHandler<UpdateConnectionCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageBroker _messageBroker;

    public UpdateConnectionCommandHandler(IUserRepository userRepository, IMessageBroker messageBroker)
    {
        _userRepository = userRepository;
        _messageBroker = messageBroker;
    }

    public async Task Handle(UpdateConnectionCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.UpdateConnectionAsync(request.UserId, request.TargetId, cancellationToken);
        await _messageBroker.PublishMessageAsync(new UserNetworkConnectionUpdated(request.UserId, request.TargetId));
    }
}