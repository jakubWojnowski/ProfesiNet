using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface IPostService
{
    Task<Guid> AddAsync(CreatePostCommand command, Guid id, CancellationToken cancellationToken = default);
    Task<PostDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PostDto>> BrowseAsync(Guid creatorId,CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PostDto>> BrowsePerCreatorAsync(Guid creatorId,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PostDto>> BrowseAllOwnAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdatePostCommand command, Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(DeletePostCommand command , Guid id, CancellationToken cancellationToken = default);
}