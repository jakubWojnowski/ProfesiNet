using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface ICommentService
{
    Task AddAsync(CommentDto commentDto,CancellationToken cancellationToken = default);
    Task<CommentDetailsDto?> GetAsync(Guid id,CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CommentDto>> BrowseAsync(Guid postId,CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateCommentDto updateCommentDto,CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken = default);
}