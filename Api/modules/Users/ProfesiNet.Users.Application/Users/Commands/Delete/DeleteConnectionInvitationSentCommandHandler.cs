using MediatR;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal class DeleteConnectionInvitationSentCommandHandler : IRequestHandler<DeleteConnectionInvitationSentCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteConnectionInvitationSentCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task Handle(DeleteConnectionInvitationSentCommand request, CancellationToken cancellationToken)
           => await _userRepository.DeleteConnectionInvitationSentAsync(request.UserId, request.TargetId, cancellationToken);
}