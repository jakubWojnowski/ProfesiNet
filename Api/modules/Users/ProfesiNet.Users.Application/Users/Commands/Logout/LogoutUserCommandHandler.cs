using MediatR;
using ProfesiNet.Users.Application.Users.Services.UserContext;

namespace ProfesiNet.Users.Application.Users.Commands.Logout;

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, bool>
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
        return await _identityService.Logout();
    }
}