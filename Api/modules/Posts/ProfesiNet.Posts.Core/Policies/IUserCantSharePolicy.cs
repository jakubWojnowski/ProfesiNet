namespace ProfesiNet.Posts.Core.Policies;

internal interface IUserCantSharePolicy
{
    Task<bool> CheckShareAsync(Guid userId, Guid postId, CancellationToken ct = default);
}