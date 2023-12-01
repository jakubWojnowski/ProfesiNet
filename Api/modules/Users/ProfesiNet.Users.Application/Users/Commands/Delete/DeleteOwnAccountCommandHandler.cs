using MediatR;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal class DeleteOwnAccountCommandHandler : IRequestHandler<DeleteOwnAccountCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IUserRepository _userRepository;

    public DeleteOwnAccountCommandHandler(IIdentityService identityService,
        ICurrentUserContextService currentUserContextService, IUserRepository userRepository)
    {
        _identityService = identityService;
        _currentUserContextService = currentUserContextService;
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteOwnAccountCommand request, CancellationToken cancellationToken)
    {
        var tokenId = _currentUserContextService.GetCurrentUser()?.Id;
        if (tokenId == null)
        {
            throw new Exception("no id in token");
        }

        var id = Guid.Parse(tokenId);
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == id, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(id);
        }
            
        
        await _userRepository.DeleteAsync(user, cancellationToken);
        
        await _identityService.Logout();
    }
}