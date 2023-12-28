using ProfesiNet.Posts.Core.Commands.Create;

namespace ProfesiNet.Posts.Core.Policies;

internal interface IPostCannotBeEmptyPolicy
{
    Task<bool> CheckPostContentAsync(CreatePostCommand postCommand, CancellationToken ct = default);
}