using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Shared.Contexts;

namespace ProfesiNet.Posts.Api.Controllers;

internal class CommentController : BaseController
{
    private readonly ICommentService _commentService;
    private readonly ICommentLikeService _commentLikeService;
    private readonly IContext _context;

    public CommentController(ICommentService commentService, ICommentLikeService commentLikeService, IContext context)
    {
        _commentService = commentService;
        _commentLikeService = commentLikeService;
        _context = context;
    }
    
    [HttpGet("GetById{id:guid}")]
    public async Task<ActionResult<CommentDetailsDto?>> Get(Guid id, CancellationToken cancellationToken ) => OkOrNotFound(await _commentService.GetAsync(id, cancellationToken));
    
    [HttpGet("GetAllPerPost/{postId:guid}")]
    public async Task<ActionResult<IReadOnlyList<CommentDto>>> BrowseAsync(Guid postId,CancellationToken cancellationToken) => Ok(await _commentService.BrowseAsync(postId, cancellationToken));
    
    [HttpPost]
    public async Task<ActionResult> AddAsync(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        await _commentService.AddAsync(command,_context.Id, cancellationToken);
        return CreatedAtAction(nameof(Get), new {id = command.Id}, null);
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateAsync( UpdateCommentCommand command, CancellationToken cancellationToken)
    {
        await _commentService.UpdateAsync(command,_context.Id, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete()]
    public async Task<ActionResult> DeleteAsync( DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        await _commentService.DeleteAsync(command,_context.Id, cancellationToken);
        return NoContent();
    }
    
    //likes
    
    [HttpGet("CommentLike/{id:guid}")]
    public async Task<ActionResult<CommentLikeDetailsDto?>> GetCommentLike(Guid id, CancellationToken cancellationToken) =>
        OkOrNotFound(await _commentLikeService.GetAsync(id, cancellationToken));


    [HttpGet("CommentLikes/{id:guid}")]
    public async Task<ActionResult<IReadOnlyList<CommentLikeDto>>> BrowseCommentLikesAsync(Guid id,CancellationToken cancellationToken) =>
        Ok(await _commentLikeService.BrowseLikesPerCommentAsync(id, cancellationToken));


    [HttpPost("CommentLike")]
    public async Task<ActionResult> AddCommentLikeAsync(CreateCommentLikeCommand command, CancellationToken cancellationToken)
    {
        await _commentLikeService.AddAsync(command,_context.Id, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = command.Id }, null);
    }


    [HttpDelete("CommentLike")]
    public async Task<ActionResult> DeleteCommentLikeAsync(DeleteCommentLikeCommand command, CancellationToken cancellationToken)
    {
        await _commentLikeService.DeleteAsync(command,_context.Id, cancellationToken);
        return NoContent();
    }
}