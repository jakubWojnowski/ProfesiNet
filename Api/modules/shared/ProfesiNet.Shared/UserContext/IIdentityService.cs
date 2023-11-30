namespace ProfesiNet.Shared.UserContext;

public interface IIdentityService
{
    Task<bool> Logout();
}