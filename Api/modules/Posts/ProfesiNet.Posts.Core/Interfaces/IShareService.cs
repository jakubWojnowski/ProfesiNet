using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface IShareService
{
    Task<Guid> AddAsync(CreatePostShareCommand command, Guid id, CancellationToken ct = default);
    Task DeleteAsync(DeletePostShareCommand command, Guid id, CancellationToken ct = default);
    Task<ShareDetailsDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<ShareDto>> BrowseSharesPerPostAsync(Guid postId, CancellationToken ct = default);
    Task<IReadOnlyList<ShareDetailsDto>> BrowseSharesPerUserAsync(Guid creatorId, CancellationToken ct = default);
}