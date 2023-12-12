namespace ProfesiNet.Posts.Core.Commands.Update;

internal record UpdatePostCommand(Guid Id, string? Media, string? Description);

