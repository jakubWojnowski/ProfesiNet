using ProfesiNet.Posts.Core.Dto;

namespace ProfesiNet.Posts.Core.Interfaces;

internal interface IPostService
{
    Task AddAsync(PostDto postDto);
    Task<PostDto?> GetAsync(Guid id);
    Task<IReadOnlyList<PostDto>> BrowseAsync();
    Task UpdateAsync(UpdatePostDto updatePostDto);
    Task DeleteAsync(Guid id);
}