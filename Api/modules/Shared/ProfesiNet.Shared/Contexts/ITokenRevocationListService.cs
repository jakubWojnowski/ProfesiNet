namespace ProfesiNet.Shared.Contexts;

public interface ITokenRevocationListService
{
    Task<bool> IsTokenRevoked(string token);
    Task<bool> RevokeToken(string token);
}