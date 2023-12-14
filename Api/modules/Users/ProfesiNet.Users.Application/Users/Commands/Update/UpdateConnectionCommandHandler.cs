using MediatR;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal class UpdateConnectionCommandHandler : IRequestHandler<UpdateConnectionCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateConnectionCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task Handle(UpdateConnectionCommand request, CancellationToken cancellationToken) 
        => await _userRepository.UpdateConnectionAsync(request.UserId, request.TargetId, cancellationToken);
}