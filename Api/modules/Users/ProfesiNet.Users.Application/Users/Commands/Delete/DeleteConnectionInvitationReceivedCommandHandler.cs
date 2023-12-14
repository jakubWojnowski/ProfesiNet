using MediatR;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal class DeleteConnectionInvitationReceivedCommandHandler : IRequestHandler<DeleteConnectionInvitationReceivedCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteConnectionInvitationReceivedCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task Handle(DeleteConnectionInvitationReceivedCommand request, CancellationToken cancellationToken)
        => await _userRepository.DeleteConnectionInvitationReceivedAsync(request.UserId, request.TargetId, cancellationToken);
}