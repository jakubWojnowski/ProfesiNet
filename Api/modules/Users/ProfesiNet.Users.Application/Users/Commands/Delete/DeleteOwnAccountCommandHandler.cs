using MediatR;
using ProfesiNet.Users.Application.Users.Services.UserContext;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

public class DeleteOwnAccountCommandHandler : IRequestHandler<DeleteOwnAccountCommand>
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
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == id, cancellationToken) ??
                   throw new Exception("Invalid user id");
        
        await _userRepository.DeleteAsync(user, cancellationToken);
        
        await _identityService.Logout();
    }
}