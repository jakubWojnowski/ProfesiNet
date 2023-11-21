using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProfesiNet.Users.Application.Users.Services.UserContect;

namespace ProfesiNet.Users.Application.Users.Services.UserContext
{
    public class CurrentUserContextService : ICurrentUserContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public CurrentUserContext? GetCurrentUser()
        {
            var user = _contextAccessor.HttpContext?.User;
            if (user == null || !user.Claims.Any())
            {
                return null;
            }

            var fullName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return new CurrentUserContext(fullName, id);
        }
    }
}