using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface IPostLikeService
{
    Task<int>  AddAsync(CreatePostLikeCommand command, Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(DeletePostLikeCommand command, Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PostLikeDto>> BrowseAsync(Guid postId, CancellationToken cancellationToken = default);
    Task<PostLikeDetailsDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
}