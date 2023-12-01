using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface IPostService
{
    Task AddAsync(PostDto postDto, CancellationToken cancellationToken = default);
    Task<PostDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PostDto>> BrowseAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdatePostDto updatePostDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}