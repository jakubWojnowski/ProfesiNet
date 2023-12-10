namespace ProfesiNet.Shared.Contexts;

public interface IIdentityService
{
    Task<bool> Logout();
    Task<bool> InvalidateUserToken(string token);
}