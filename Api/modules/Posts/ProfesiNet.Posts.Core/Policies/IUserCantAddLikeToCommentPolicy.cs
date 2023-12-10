namespace ProfesiNet.Posts.Core.Policies;

internal interface IUserCantAddLikeToCommentPolicy
{
    Task<bool> CheckCommentLikeAsync(Guid userId, Guid commentId, CancellationToken ct = default);
}