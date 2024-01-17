namespace ProfesiNet.Shared.Contexts;

public class TokenRevocationListService : ITokenRevocationListService
{
    private readonly HashSet<string> _revokedTokens = new HashSet<string>();

    public Task<bool> IsTokenRevoked(string token)
    {
        return Task.FromResult(_revokedTokens.Contains(token));
    }

    public Task<bool> RevokeToken(string token)
    {
        _revokedTokens.Add(token);
        return Task.FromResult(true);
        
    }
}