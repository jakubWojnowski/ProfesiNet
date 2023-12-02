namespace ProfesiNet.Posts.Core.Commands.Create;

internal record CreatePostShareCommand(Guid PostId)
{
    public Guid Id { get; init; }
}