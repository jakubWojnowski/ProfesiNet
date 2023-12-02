namespace ProfesiNet.Posts.Core.Commands.Update;

internal record UpdateCommentCommand(string? Content)
{
    public Guid Id { get; init; }
}

