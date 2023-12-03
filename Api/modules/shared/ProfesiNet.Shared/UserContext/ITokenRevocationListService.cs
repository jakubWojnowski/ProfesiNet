namespace ProfesiNet.Shared.UserContext;

public interface ITokenRevocationListService
{
    Task<bool> IsTokenRevoked(string token);
    Task<bool> RevokeToken(string token);
}