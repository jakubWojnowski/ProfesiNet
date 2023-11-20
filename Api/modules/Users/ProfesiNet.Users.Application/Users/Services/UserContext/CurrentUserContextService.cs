using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ProfesiNet.Users.Application.Users.Services.UserContect;

namespace ProfesiNet.Users.Application.Users.Services.UserContext;

public class CurrentUserContextService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserContextService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public CurrentUserContext? GetCurrentUser()
    {
        var claims = _contextAccessor.HttpContext?.User.Claims;
        if (claims == null)
        {
            return null;
        }

        var enumerable = claims as Claim[] ?? claims.ToArray();
        var fullname = enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        var id = enumerable.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        return new CurrentUserContext(fullname,id);
    }
}