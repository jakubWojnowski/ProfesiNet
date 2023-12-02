namespace ProfesiNet.Posts.Core.Commands.Update;

public record UpdateCommentCommand(string? Content)
{
    public Guid Id { get; init; }
}

