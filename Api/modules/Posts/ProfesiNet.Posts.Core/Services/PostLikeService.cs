using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Exceptions;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Mappings;
using ProfesiNet.Posts.Core.Policies;
using ProfesiNet.Posts.Core.Repositories;

namespace ProfesiNet.Posts.Core.Services;

internal class PostLikeService : IPostLikeService
{
    private readonly IPostLikeRepository _postLikeRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserCantAddLikeToPostPolicy _userCantAddLikeToPostPolicy;
    private static readonly PostLikeMapper Mapper = new();

    public PostLikeService(IPostLikeRepository postLikeRepository, IPostRepository postRepository,
        IUserCantAddLikeToPostPolicy userCantAddLikeToPostPolicy)
    {
        _postLikeRepository = postLikeRepository;
        _postRepository = postRepository;
        _userCantAddLikeToPostPolicy = userCantAddLikeToPostPolicy;
    }

    public async Task AddAsync(PostLikeDetailsDto postLikeDetailsDto, CancellationToken cancellationToken = default)
    {
        var post = await _postRepository.GetByIdAsync(postLikeDetailsDto.PostId, cancellationToken);
        var postLike = Mapper.MapPostLikeDetailsDtoToPostLike(postLikeDetailsDto);
        if (post is null)
        {
            throw new PostNotFoundException(postLikeDetailsDto.PostId);
        }

        if (!await _userCantAddLikeToPostPolicy.CheckPostLikeAsync(postLikeDetailsDto.CreatorId,
                postLikeDetailsDto.PostId, cancellationToken))
        {
            throw new UserCannotLikeException(postLike.CreatorId, postLike.PostId);
        }

        postLike.Id = Guid.NewGuid();


        await _postLikeRepository.AddAsync(postLike, cancellationToken);
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var postLike = await _postLikeRepository.GetByIdAsync(id, cancellationToken);
        if (postLike is null)
        {
            throw new PostLikeNotFoundException(id);
        }

        await _postLikeRepository.DeleteAsync(postLike, cancellationToken);
    }
    
    public async Task<IReadOnlyList<PostLikeDto>> BrowseAsync(Guid postId, CancellationToken cancellationToken = default)
    {
        var postLikes = await _postLikeRepository.GetAllForConditionAsync(p => p.PostId == postId, cancellationToken);
        return Mapper.MapPostLikeToPostLikeDto(postLikes);
    }
    
    public async Task<PostLikeDetailsDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var postLike = await _postLikeRepository.GetByIdAsync(id, cancellationToken);
        if (postLike is null)
        {
            throw new PostLikeNotFoundException(id);
        }

        var dto = Mapper.MapPostLikeToPostLikeDetailsDto(postLike);
        return dto;
    }
}