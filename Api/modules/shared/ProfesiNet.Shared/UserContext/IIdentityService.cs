namespace ProfesiNet.Users.Application.UserContext;

public interface IIdentityService
{
    Task<bool> Logout();
}