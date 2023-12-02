namespace ProfesiNet.Posts.Core.Commands.Update;

internal record UpdatePostCommand(string? Media, string? Description)
{
    public Guid Id { get; init; }
}
