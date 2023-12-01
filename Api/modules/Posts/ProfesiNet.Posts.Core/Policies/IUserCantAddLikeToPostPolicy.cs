namespace ProfesiNet.Posts.Core.Policies;

internal interface IUserCantAddLikeToPostPolicy
{
    Task<bool> CheckPostLikeAsync(Guid userId, Guid postId, CancellationToken ct = default);
}