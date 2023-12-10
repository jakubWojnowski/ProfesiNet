using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
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

    public async Task<Guid> AddAsync(CreateCommentLikeCommand command, Guid id, CancellationToken ct = default)
    {
        var creatorId = id;
        var comment = await _commentRepository.GetByIdAsync(command.CommentId, ct);
        if (comment is null)
        {
            throw new CommentNotFoundException(command.CommentId);
        }

        var commentLike = Mapper.MapCreatePostLikeCommandToComment(command with
        {
            Id = Guid.NewGuid(),
        });
        commentLike.CreatorId = creatorId;

        if (!await _userCantAddLikeToCommentPolicy.CheckCommentLikeAsync(creatorId,
                command.CommentId, ct))
        {
            throw new UserCannotLikeException(commentLike.CreatorId, commentLike.CommentId);
        }


        return await _commentLikeRepository.AddAsync(commentLike, ct);
    }

    public async Task DeleteAsync(DeleteCommentLikeCommand command, Guid id, CancellationToken ct = default)
    {
        var creatorId = id;
        var commentLike =
            await _commentLikeRepository.GetRecordByFilterAsync(
                l => l.Id == command.Id && l.CreatorId == creatorId, ct);
        if (commentLike is null)
        {
            throw new CommentLikeNotFoundException(command.Id);
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

    public async Task<IReadOnlyList<CommentLikeDto>> BrowseLikesPerCommentAsync(Guid commentId,
        CancellationToken ct = default)
    {
        var commentLikes = await _commentLikeRepository.GetAllForConditionAsync(l => l.CommentId == commentId, ct);

        return Mapper.MapCommentLikeToCommentLikeDto(commentLikes);
    }
}