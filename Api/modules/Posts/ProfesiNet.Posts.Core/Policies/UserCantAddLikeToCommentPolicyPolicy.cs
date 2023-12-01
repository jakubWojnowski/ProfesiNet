using ProfesiNet.Posts.Core.Entities;
using ProfesiNet.Posts.Core.Interfaces;

namespace ProfesiNet.Posts.Core.Policies;

internal class UserCantAddLikeToCommentPolicyPolicy : IUserCantAddLikeToCommentPolicy
{
    private readonly ICommentLikeRepository _commentLikeRepository;

    public UserCantAddLikeToCommentPolicyPolicy(ICommentLikeRepository commentLikeRepository)
    {
        _commentLikeRepository = commentLikeRepository;
    }
    
    public async Task<bool> CheckCommentLikeAsync(Guid userId, Guid commentId, CancellationToken ct = default)
    {
        var commentLike = await _commentLikeRepository.GetRecordByFilterAsync(
            l => l.CreatorId == userId && l.CommentId == commentId, ct);
        
        return commentLike == null;
    }
}