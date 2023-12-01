using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Exceptions;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Mappings;

namespace ProfesiNet.Posts.Core.Services;

internal class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private static readonly PostMapper Mapper = new();

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task AddAsync(PostDto postDto)
    {
        var post = Mapper.MapPostDtoToPost(postDto);
        post.Id = Guid.NewGuid();
        await _postRepository.AddAsync(post);
    }

    public async Task<PostDto?> GetAsync(Guid id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post is null)
        {
            throw new PostNotFoundException(id);
        }

        var dto = Mapper.MapPostToPostDto(post);
        return dto;
    }

    public async Task<IReadOnlyList<PostDto>> BrowseAsync()
    {
        var posts = await _postRepository.GetAllAsync();
        return Mapper.MapPostsToPostDtos(posts);
    }

    public async Task UpdateAsync(UpdatePostDto updatePostDto)
    {
        var post = await _postRepository.GetByIdAsync(updatePostDto.Id);
        if (post is null)
        {
            throw new PostNotFoundException(updatePostDto.Id);
        }

        post.Description = updatePostDto.Description;
        post.Media = updatePostDto.Media;
        post.PublishedAt = updatePostDto.CreatedAt;
        await _postRepository.UpdateAsync(post);
    }

    public async Task DeleteAsync(Guid id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post is null)
        {
            throw new PostNotFoundException(id);
        }
        
        await _postRepository.DeleteAsync(post);
    }
}