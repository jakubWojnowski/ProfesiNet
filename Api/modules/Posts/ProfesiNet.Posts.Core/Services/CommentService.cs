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

internal class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IClock _clock;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly CommentMapper Mapper = new();

    public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, IClock clock, ICurrentUserContextService currentUserContextService)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _clock = clock;
        _currentUserContextService = currentUserContextService;
    }

    public async Task<Guid> AddAsync(CreateCommentCommand command, CancellationToken cancellationToken = default)
    {
        var post = await _postRepository.GetByIdAsync(command.PostId, cancellationToken);
        if (post is null)
        {
            throw new PostNotFoundException(command.PostId);
        }
        
        var comment = Mapper.MapCreateCommentCommandToComment(command with
        {
            Id = Guid.NewGuid()
        });
        comment.PublishedAt = _clock.CurrentDate();
        comment.CreatorId = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);

       return await _commentRepository.AddAsync(comment, cancellationToken);
    }

    public async Task<CommentDetailsDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var comment = await _commentRepository.GetByIdAsync(id, cancellationToken);
        if (comment is null)
        {
            throw new CommentNotFoundException(id);
        }

        var dto = Mapper.MapCommentToCommentDetailsDto(comment);
        return dto;
    }

    public async Task<IReadOnlyList<CommentDto>> BrowseAsync(Guid postId,CancellationToken cancellationToken = default)
    {
        var comments = await _commentRepository.GetAllForConditionAsync(c => c.PostId == postId, cancellationToken);
        return Mapper.MapCommentToCommentDto(comments);
    }

    public async Task UpdateAsync(UpdateCommentCommand command, CancellationToken cancellationToken = default)
    {
        var creatorId = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);
        var comment = await _commentRepository.GetRecordByFilterAsync(c => c.Id == command.Id && c.CreatorId == creatorId, cancellationToken);
        if (comment is null)
        {
            throw new CommentNotFoundException(command.Id);
        }

        var commentUpdated = Mapper.MapAndUpdateCommentCommandToComment(comment, command);
        await _commentRepository.UpdateAsync(commentUpdated, cancellationToken);
    }

    public async Task DeleteAsync(DeleteCommentCommand command, CancellationToken cancellationToken = default)
    {
        var creatorId = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);
        var comment = await _commentRepository.GetRecordByFilterAsync(c => c.Id == command.Id && c.CreatorId == creatorId, cancellationToken);
        if (comment is null)
        {
            throw new CommentNotFoundException(command.Id);
        }

        await _commentRepository.DeleteAsync(comment, cancellationToken);
    }
}