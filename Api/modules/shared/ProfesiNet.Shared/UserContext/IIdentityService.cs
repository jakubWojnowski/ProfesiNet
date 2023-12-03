namespace ProfesiNet.Shared.UserContext;

public interface IIdentityService
{
    Task<bool> Logout();
    Task<bool> InvalidateUserToken(string token);
}