using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface IShareService
{
    Task AddAsync(ShareDetailsDto shareDetailsDto, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<ShareDetailsDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<ShareDto>> BrowseAsync(Guid postId, CancellationToken ct = default);
}