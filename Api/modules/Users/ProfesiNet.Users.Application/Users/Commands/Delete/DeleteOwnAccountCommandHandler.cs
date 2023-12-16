using MediatR;
using ProfesiNet.Shared.Messaging;
using ProfesiNet.Users.Application.Events;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal class DeleteOwnAccountCommandHandler : IRequestHandler<DeleteOwnAccountCommand>
{

    private readonly IUserRepository _userRepository;
    private readonly IMessageBroker _messageBroker;

    public DeleteOwnAccountCommandHandler(IUserRepository userRepository, IMessageBroker messageBroker)
    {
        _userRepository = userRepository;
        _messageBroker = messageBroker;
    }

    public async Task Handle(DeleteOwnAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.Id, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(request.Id);
        }
        await _userRepository.DeleteAsync(user, cancellationToken);
        await _messageBroker.PublishAsync(new UserDeleted(user.Id));
        
        
    }
}