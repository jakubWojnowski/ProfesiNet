namespace ProfesiNet.Shared.UserContext;

public interface ICurrentUserContextService
{
    CurrentUserContext? GetCurrentUser();
}