using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace ProfesiNet.Shared.UserContext;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITokenRevocationListService _tokenRevocationListService;

    public IdentityService(IHttpContextAccessor httpContextAccessor, ITokenRevocationListService tokenRevocationListService)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenRevocationListService = tokenRevocationListService;
    }


    public async Task<bool> Logout()
    {
        if (_httpContextAccessor.HttpContext == null) return true;
        var token = _httpContextAccessor.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        if (!string.IsNullOrEmpty(token))
        {
            await _tokenRevocationListService.RevokeToken(token);
        }
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return true;
    }
    
    public async Task<bool> InvalidateUserToken(string token)
    {
        return await _tokenRevocationListService.RevokeToken(token);
    }
    
    
    
}