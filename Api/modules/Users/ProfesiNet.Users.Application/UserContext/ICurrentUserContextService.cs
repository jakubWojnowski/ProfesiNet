namespace ProfesiNet.Users.Application.UserContext;

public interface ICurrentUserContextService
{
    CurrentUserContext? GetCurrentUser();
}