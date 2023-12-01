using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface IPostLikeService
{
    Task AddAsync(PostLikeDetailsDto postLikeDetailsDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PostLikeDto>> BrowseAsync(Guid postId, CancellationToken cancellationToken = default);
    Task<PostLikeDetailsDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
}