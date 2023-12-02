using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface IPostService
{
    Task<Guid> AddAsync(CreatePostCommand command, CancellationToken cancellationToken = default);
    Task<PostDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PostDto>> BrowseAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdatePostCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(DeletePostCommand command, CancellationToken cancellationToken = default);
}