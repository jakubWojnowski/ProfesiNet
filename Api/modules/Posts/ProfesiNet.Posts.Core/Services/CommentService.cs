using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Exceptions;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Mappings;

namespace ProfesiNet.Posts.Core.Services;

internal class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private static readonly CommentMapper Mapper = new();

    public CommentService(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }

    public async Task AddAsync(CommentDto commentDto, CancellationToken cancellationToken = default)
    {
        var post = await _postRepository.GetByIdAsync(commentDto.PostId, cancellationToken);
        if (post is null)
        {
            throw new PostNotFoundException(commentDto.PostId);
        }

        commentDto.Id = Guid.NewGuid();
        var comment = Mapper.MapCommentDtoToComment(commentDto);

        await _commentRepository.AddAsync(comment, cancellationToken);
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

    public async Task UpdateAsync(UpdateCommentDto updateCommentDto, CancellationToken cancellationToken = default)
    {
        var comment = await _commentRepository.GetByIdAsync(updateCommentDto.Id, cancellationToken);
        if (comment is null)
        {
            throw new CommentNotFoundException(updateCommentDto.Id);
        }

        var commentUpdated = Mapper.MapAndUpdateCommentDtoToComment(comment, updateCommentDto);
        await _commentRepository.UpdateAsync(commentUpdated, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var comment = await _commentRepository.GetByIdAsync(id, cancellationToken);
        if (comment is null)
        {
            throw new CommentNotFoundException(id);
        }

        await _commentRepository.DeleteAsync(comment, cancellationToken);
    }
}