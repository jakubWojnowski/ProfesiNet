using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Exceptions;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Mappings;
using ProfesiNet.Posts.Core.Policies;

namespace ProfesiNet.Posts.Core.Services;

internal class CommentLikeService : ICommentLikeService
{
    private readonly ICommentLikeRepository _commentLikeRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserCantAddLikeToCommentPolicy _userCantAddLikeToCommentPolicy;
    private static readonly CommentLikeMapper Mapper = new();

    public CommentLikeService(ICommentLikeRepository commentLikeRepository, ICommentRepository commentRepository,
        IUserCantAddLikeToCommentPolicy userCantAddLikeToCommentPolicy)
    {
        _commentLikeRepository = commentLikeRepository;
        _commentRepository = commentRepository;
        _userCantAddLikeToCommentPolicy = userCantAddLikeToCommentPolicy;
    }

    public async Task AddAsync(CommentLikeDetailsDto commentLikeDetailsDto, CancellationToken ct = default)
    {
        var commentLikeDto = Mapper.MapCommentLikeDetailsDtoToComment(commentLikeDetailsDto);
        var comment = await _commentRepository.GetByIdAsync(commentLikeDetailsDto.CommentId, ct);
        commentLikeDto.Id = Guid.NewGuid();
        if (comment is null)
        {
            throw new CommentNotFoundException(commentLikeDetailsDto.CommentId);
        }

        if (!await _userCantAddLikeToCommentPolicy.CheckCommentLikeAsync(commentLikeDetailsDto.CreatorId,
                commentLikeDetailsDto.CommentId, ct))
        {
            throw new UserCannotLikeException(commentLikeDetailsDto.CreatorId, commentLikeDetailsDto.CommentId);
        }
       

        await _commentLikeRepository.AddAsync(commentLikeDto, ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var commentLike = await _commentLikeRepository.GetByIdAsync(id, ct);
        if (commentLike is null)
        {
            throw new CommentLikeNotFoundException(id);
        }

        await _commentLikeRepository.DeleteAsync(commentLike, ct);
    }

    public async Task<CommentLikeDetailsDto?> GetAsync(Guid id, CancellationToken ct = default)
    {
        var commentLike = await _commentLikeRepository.GetByIdAsync(id, ct);
        if (commentLike is null)
        {
            throw new CommentLikeNotFoundException(id);
        }

        return Mapper.MapCommentLikeToCommentLikeDetailsDto(commentLike);
    }

    public async Task<IReadOnlyList<CommentLikeDto>> BrowseAsync(Guid commentId,CancellationToken ct = default)
    {
        var commentLikes = await _commentLikeRepository.GetAllForConditionAsync(l => l.CommentId == commentId, ct);
        
        return Mapper.MapCommentLikeToCommentLikeDto(commentLikes);
    }
}