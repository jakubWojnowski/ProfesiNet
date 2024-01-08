namespace ProfesiNet.Posts.Core.Commands.Create;

public record CreateCommentCommand(string? Content, Guid PostId)
{
    public Guid CommentId { get; init; }
}