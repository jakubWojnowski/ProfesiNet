namespace ProfesiNet.Users.Application.Users.Services.UserContext;

public interface IIdentityService
{
    Task<bool> Logout();
}