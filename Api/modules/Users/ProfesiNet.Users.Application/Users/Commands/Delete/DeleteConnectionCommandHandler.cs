using MediatR;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal class DeleteConnectionCommandHandler : IRequestHandler<DeleteConnectionCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteConnectionCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task Handle(DeleteConnectionCommand request, CancellationToken cancellationToken)
        => await _userRepository.DeleteConnectionAsync(request.UserId, request.TargetId, cancellationToken);
}