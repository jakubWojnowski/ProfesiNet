using Confab.Shared.Abstractions.Interfaces;
using Microsoft.IdentityModel.Tokens;
using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Exceptions;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Mappings;
using ProfesiNet.Shared.Contexts;

namespace ProfesiNet.Posts.Core.Services;

internal class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IClock _clock;
    private readonly ICreatorRepository _creatorRepository;
    private readonly IContext _context;
    private static readonly CommentMapper Mapper = new();

    public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, IClock clock, ICreatorRepository creatorRepository, IContext context)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _clock = clock;
        _creatorRepository = creatorRepository;
        _context = context;
    }

    public async Task<CommentDto> AddAsync(CreateCommentCommand command,Guid creatorId, CancellationToken cancellationToken = default)
    {
        var post = await _postRepository.GetByIdAsync(command.PostId, cancellationToken);
        var creator = await _creatorRepository.GetByIdAsync(_context.Id, cancellationToken)?? throw new CreatorNotFoundException(_context.Id);
        if (post is null)
        {
            throw new PostNotFoundException(command.PostId);
        }

        if (command.Content.IsNullOrEmpty()) throw new CommentHasNoContentException();
        
        var comment = Mapper.MapCreateCommentCommandToComment(command with
        {
            CommentId = Guid.NewGuid()
        });
        comment.PublishedAt = _clock.CurrentDate();
        
        comment.CreatorId = creatorId;

       var commentId= await _commentRepository.AddAsync(comment, cancellationToken);
        var commentDto = new CommentDto
        {
            Id = commentId,
            CreatorId = comment.CreatorId,
            CreatorName = creator.Name,
            CreatorSurname = creator.Surname,
            CreatorProfilePicture = creator.ProfilePicture,
            PostId = comment.PostId,
            Content = comment.Content,
            PublishedAt = comment.PublishedAt
        };

        return commentDto;
    }

    public async Task<CommentDetailsDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var comment = await _commentRepository.GetCommentWithCreator(id, cancellationToken);
        if (comment is null)
        {
            throw new CommentNotFoundException(id);
        }

        var dto = Mapper.MapCommentToCommentDetailsDto(comment);
        return dto;
    }

    public async Task<IReadOnlyList<CommentDto>> BrowseAsync(Guid postId,CancellationToken cancellationToken = default)
    {
        var comments = await _commentRepository.GetCommentsWithCreatorsPerPost(postId, cancellationToken);
        var dtos = Mapper.MapCommentToCommentDto(comments);
        foreach (var comment in dtos)
        {
            comment.CreatorProfilePicture = comment.CreatorProfilePicture;
        

        }
      
        return dtos;
    }

    public async Task UpdateAsync(UpdateCommentCommand command, Guid id, CancellationToken cancellationToken = default)
    {
        var creatorId = id;
        var comment = await _commentRepository.GetRecordByFilterAsync(c => c.Id == command.Id && c.CreatorId == creatorId, cancellationToken);
        if (comment is null)
        {
            throw new CommentNotFoundException(command.Id);
        }

        var commentUpdated = Mapper.MapAndUpdateCommentCommandToComment(command, comment);
        await _commentRepository.UpdateAsync(commentUpdated, cancellationToken);
    }

    public async Task DeleteAsync(DeleteCommentCommand command, Guid creatorId, CancellationToken cancellationToken = default)
    {
        var comment = await _commentRepository.GetRecordByFilterAsync(c => c.Id == command.Id && c.CreatorId == creatorId, cancellationToken);
        if (comment is null)
        {
            throw new CommentNotFoundException(command.Id);
        }

        await _commentRepository.DeleteAsync(comment, cancellationToken);
    }
}