using MediatR;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal class UpdateConnectionInvitationCommandHandler : IRequestHandler<UpdateConnectionInvitationCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateConnectionInvitationCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task Handle(UpdateConnectionInvitationCommand request, CancellationToken cancellationToken) 
        => await _userRepository.UpdateConnectionInvitationsAsync(request.UserId, request.TargetId, cancellationToken);
}