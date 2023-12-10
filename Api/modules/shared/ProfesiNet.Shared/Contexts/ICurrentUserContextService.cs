namespace ProfesiNet.Shared.Contexts;

public interface ICurrentUserContextService
{
    CurrentUserContext? GetCurrentUser();
}