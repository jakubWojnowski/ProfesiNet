using MediatR;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal class DeleteOwnAccountCommandHandler : IRequestHandler<DeleteOwnAccountCommand>
{

    private readonly IUserRepository _userRepository;

    public DeleteOwnAccountCommandHandler(IUserRepository userRepository)
    {
      
      
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteOwnAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.Id, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(request.Id);
        }
        await _userRepository.DeleteAsync(user, cancellationToken);
        
    }
}