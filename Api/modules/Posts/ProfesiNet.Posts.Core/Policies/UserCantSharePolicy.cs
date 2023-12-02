using ProfesiNet.Posts.Core.Interfaces;

namespace ProfesiNet.Posts.Core.Policies;

internal class UserCantSharePolicy : IUserCantSharePolicy
{
    private readonly IShareRepository _shareRepository;

    public UserCantSharePolicy(IShareRepository shareRepository)
    {
        _shareRepository = shareRepository;
    }
    
    public async Task<bool> CheckShareAsync(Guid userId, Guid postId, CancellationToken ct = default)
    {
        var share = await _shareRepository.GetRecordByFilterAsync(
            p => p.CreatorId == userId && p.PostId == postId, ct);
        return share == null;
    }
}