using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface ICommentLikeService
{
    Task AddAsync(CommentLikeDetailsDto commentLikeDetailsDto, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<CommentLikeDetailsDto?> GetAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<CommentLikeDto>> BrowseAsync(Guid commentId, CancellationToken ct = default);
}