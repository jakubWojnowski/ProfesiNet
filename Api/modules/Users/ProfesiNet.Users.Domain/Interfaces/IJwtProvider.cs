using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Domain.Interfaces;

public interface IJwtProvider
{
    string GenerateJwtToken(User user);
}