using MediatR;
using ProfesiNet.Shared.UserContext;

namespace ProfesiNet.Users.Application.Users.Commands.Logout;

internal class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, bool>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserContextService _currentUserContext;

    public LogoutUserCommandHandler(IIdentityService identityService, ICurrentUserContextService currentUserContext)
    {
        _identityService = identityService;
        _currentUserContext = currentUserContext;
    }
    public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var user = _currentUserContext.GetCurrentUser();
        if (user == null)
        {
            return false;
        }
        
        var isTokenInvalidated = await _identityService.InvalidateUserToken(user.Token);
        if (isTokenInvalidated)
        {
            await _identityService.Logout();
        }

        return isTokenInvalidated;
    }
}