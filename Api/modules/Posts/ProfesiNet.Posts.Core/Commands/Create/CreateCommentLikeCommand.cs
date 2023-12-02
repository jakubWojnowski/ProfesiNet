namespace ProfesiNet.Posts.Core.Commands.Create;

internal record CreateCommentLikeCommand(Guid CommentId)
{
    public Guid Id { get; set; }
}