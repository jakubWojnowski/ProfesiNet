using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface ICommentLikeService
{
    Task<Guid>  AddAsync(CreateCommentLikeCommand command, CancellationToken ct = default);
    Task DeleteAsync(DeleteCommentLikeCommand command, CancellationToken ct = default);
    Task<CommentLikeDetailsDto?> GetAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<CommentLikeDto>> BrowseLikesPerCommentAsync(Guid commentId,CancellationToken ct = default);
}