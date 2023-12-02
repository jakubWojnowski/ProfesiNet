namespace ProfesiNet.Posts.Core.Commands.Create;

internal record CreatePostLikeCommand(Guid PostId)
{
    public Guid Id { get; init; }
}
