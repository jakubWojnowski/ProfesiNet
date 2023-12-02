namespace ProfesiNet.Posts.Core.Commands.Update;

public record UpdatePostCommand(string? Media, string? Description)
{
    public Guid Id { get; init; }
}
