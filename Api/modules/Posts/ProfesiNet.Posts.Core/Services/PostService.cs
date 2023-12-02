using Confab.Shared.Abstractions.Interfaces;
using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Exceptions;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Mappings;
using ProfesiNet.Shared.UserContext;

namespace ProfesiNet.Posts.Core.Services;

internal class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IClock _clock;
    private static readonly PostMapper Mapper = new();

    public PostService(IPostRepository postRepository, ICurrentUserContextService currentUserContextService,
        IClock clock)
    {
        _postRepository = postRepository;
        _currentUserContextService = currentUserContextService;
        _clock = clock;
    }

    public async Task<Guid> AddAsync(CreatePostCommand command, CancellationToken cancellationToken = default)
    {
        var post = Mapper.MapCreatePostCommandToPost(command with
        {
            Id = Guid.NewGuid(),
        });
        post.PublishedAt = _clock.CurrentDate();
        post.CreatorId = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);

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
        return Mapper.MapPostsToPostDtos(posts);
    }
    public async Task<IReadOnlyList<PostDto>> BrowsePerCreatorAsync(Guid creatorId,CancellationToken cancellationToken = default)
    {
        var posts = await _postRepository.GetAllForConditionAsync(p => p.CreatorId == creatorId, cancellationToken);
        return Mapper.MapPostsToPostDtos(posts);
    }
    public async Task<IReadOnlyList<PostDto>> BrowseAllOwnAsync(CancellationToken cancellationToken = default)
    {
        var creatorId = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);
        var posts = await _postRepository.GetAllForConditionAsync(p => p.CreatorId == creatorId, cancellationToken);
        return Mapper.MapPostsToPostDtos(posts);
    }
    
    

    public async Task UpdateAsync(UpdatePostCommand command, CancellationToken cancellationToken = default)
    {
        var creatorId = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);
        var post = await _postRepository.GetRecordByFilterAsync(p => p.CreatorId == creatorId && p.Id == command.Id,
            cancellationToken);
        if (post is null)
        {
            throw new PostNotFoundException(command.Id);
        }

        var updatedPost = Mapper.MapAndUpdateUpdatePostCommandToPost(command);
        await _postRepository.UpdateAsync(updatedPost, cancellationToken);
    }

    public async Task DeleteAsync(DeletePostCommand command, CancellationToken cancellationToken = default)
    {
        var creatorId = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);
        var post = await _postRepository.GetRecordByFilterAsync(p => p.CreatorId == creatorId && p.Id == command.PostId,
            cancellationToken);
        if (post is null)
        {
            throw new PostNotFoundException(command.PostId);
        }

        await _postRepository.DeleteAsync(post, cancellationToken);
    }
}

// to do: check if creator exists
// if(_CreatorRepository.GetByIdAsync(post.CreatorId, cancellationToken) is null)
// {
//     throw new CreatorNotFoundException(post.CreatorId);
// }