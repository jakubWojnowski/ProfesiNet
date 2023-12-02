using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface ICommentService
{
    Task<Guid>  AddAsync(CreateCommentCommand command, CancellationToken cancellationToken = default);
    Task<CommentDetailsDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CommentDto>> BrowseAsync(Guid postId,CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateCommentCommand command, CancellationToken cancellationToken = default);
    Task DeleteAsync(DeleteCommentCommand command, CancellationToken cancellationToken = default);
}