namespace ProfesiNet.Posts.Core.Commands.Create;

internal record CreatePostCommand(string? Media, string? Description)
{
    public Guid Id { get; init; }
}