using ProfesiNet.Posts.Core.Interfaces;

namespace ProfesiNet.Posts.Core.Policies;

internal class UserCantAddLikeToPostPolicy : IUserCantAddLikeToPostPolicy
{
    private readonly IPostLikeRepository _postLikeRepository;

    public UserCantAddLikeToPostPolicy(IPostLikeRepository postLikeRepository)
    {
        _postLikeRepository = postLikeRepository;
   
    }
    
    public async Task<bool> CheckPostLikeAsync(Guid userId, Guid postId, CancellationToken ct = default)
    {
        var postLike = await _postLikeRepository.GetRecordByFilterAsync(
            p => p.CreatorId == userId && p.PostId == postId, ct);
        return postLike == null;
    }
}