using Confab.Shared.Abstractions.Interfaces;
using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Exceptions;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Mappings;

namespace ProfesiNet.Posts.Core.Services;

internal class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IClock _clock;
    private readonly ICreatorRepository _creatorRepository;
    private static readonly PostMapper Mapper = new();

    public PostService(IPostRepository postRepository,  
        IClock clock, ICreatorRepository creatorRepository)
    {
        _postRepository = postRepository;
        _clock = clock;
        _creatorRepository = creatorRepository;
    }

    public async Task<Guid> AddAsync(CreatePostCommand command, Guid id, CancellationToken cancellationToken = default)
    {
        var post = Mapper.MapCreatePostCommandToPost(command);
        post.PublishedAt = _clock.CurrentDate();
        var creator = await _creatorRepository.GetByIdAsync(id, cancellationToken);
        if (creator is null)
        {
            throw new CreatorNotFoundException(id);
        }
        post.CreatorId = creator.Id;

        return await _postRepository.AddAsync(post, cancellationToken);
    }

    public async Task<PostDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var post = await _postRepository.GetByIdAsync(id, cancellationToken);
        if (post is null)
        {
            throw new PostNotFoundException(id);
        }

        var dto = Mapper.MapPostToPostDto(post);
        return dto;
    }

    public async Task<IReadOnlyList<PostDto>> BrowseAsync(CancellationToken cancellationToken = default)
    {
        var posts = await _postRepository.GetAllAsync(cancellationToken);
        var mappedPosts = Mapper.MapPostsToPostDtos(posts).OrderByDescending(p => p.PublishedAt).ToList();
        return mappedPosts;
    }

    public async Task<IReadOnlyList<PostDto>> BrowsePerCreatorAsync(Guid creatorId,
        CancellationToken cancellationToken = default)
    {
        var creator = await _creatorRepository.GetByIdAsync(creatorId, cancellationToken);
        if (creator is null)
        {
            throw new CreatorNotFoundException(creatorId);
        }
        var posts = await _postRepository.GetAllForConditionAsync(p => p.CreatorId == creator.Id, cancellationToken);
        return Mapper.MapPostsToPostDtos(posts);
    }

    public async Task<IReadOnlyList<PostDto>> BrowseAllOwnAsync(Guid id,CancellationToken cancellationToken = default)
    {
        var creator = await _creatorRepository.GetByIdAsync(id, cancellationToken);
        if (creator is null)
        {
            throw new CreatorNotFoundException(id);
        }
        var posts = await _postRepository.GetAllForConditionAsync(p => p.CreatorId == creator.Id, cancellationToken);
        return Mapper.MapPostsToPostDtos(posts);
    }


    public async Task UpdateAsync(UpdatePostCommand command, Guid id, CancellationToken cancellationToken = default)
    {
        var creator = await _creatorRepository.GetByIdAsync(id, cancellationToken);
        if (creator is null)
        {
            throw new CreatorNotFoundException(id);
        }
        var post = await _postRepository.GetRecordByFilterAsync(p => p.CreatorId == creator.Id && p.Id == command.Id,
            cancellationToken);
        
        if (post is null)
        {
            throw new PostNotFoundException(command.Id);
        }

        var updatedPost = Mapper.MapUpdatePostCommandToPost(command, post);
        await _postRepository.UpdateAsync(updatedPost, cancellationToken);
    }

    public async Task DeleteAsync(Guid postId, Guid userId, CancellationToken cancellationToken = default)
    {
        var creator = await _creatorRepository.GetByIdAsync(userId, cancellationToken);
        if (creator is null)
        {
            throw new CreatorNotFoundException(userId);
        }
        var post = await _postRepository.GetRecordByFilterAsync(p => p.CreatorId == creator.Id && p.Id == postId,
            cancellationToken);
        
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }

        await _postRepository.DeleteAsync(post, cancellationToken);
    }
}

// to do: check if creator exists
// if(_CreatorRepository.GetByIdAsync(post.CreatorId, cancellationToken) is null)
// {
//     throw new CreatorNotFoundException(post.CreatorId);
// }