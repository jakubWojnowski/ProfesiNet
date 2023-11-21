using ProfesiNet.Users.Application.Users.Services.UserContect;

namespace ProfesiNet.Users.Application.Users.Services.UserContext;

public interface ICurrentUserContextService
{
    CurrentUserContext? GetCurrentUser();
}