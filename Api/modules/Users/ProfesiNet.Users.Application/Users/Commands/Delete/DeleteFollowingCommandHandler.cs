using MediatR;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal class DeleteFollowingCommandHandler : IRequestHandler<DeleteFollowingCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteFollowingCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteFollowingCommand request, CancellationToken cancellationToken)
        => await _userRepository.DeleteFollowingAsync(request.UserId, request.TargetId, cancellationToken);
}