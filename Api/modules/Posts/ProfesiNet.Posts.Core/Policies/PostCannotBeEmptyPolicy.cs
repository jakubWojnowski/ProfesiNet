using ProfesiNet.Posts.Core.Commands.Create;

namespace ProfesiNet.Posts.Core.Policies;

internal class PostCannotBeEmptyPolicy : IPostCannotBeEmptyPolicy
{
  public Task<bool> CheckPostContentAsync(CreatePostCommand postCommand, CancellationToken ct = default)
  {
      return Task.FromResult(postCommand.File is null &&
                             string.IsNullOrWhiteSpace(postCommand.Description));
  }
}