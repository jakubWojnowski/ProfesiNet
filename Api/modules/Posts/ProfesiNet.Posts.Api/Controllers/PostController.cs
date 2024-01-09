using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Posts.Core.Commands.Create;
using ProfesiNet.Posts.Core.Commands.Delete;
using ProfesiNet.Posts.Core.Commands.Update;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Shared.Contexts;


namespace ProfesiNet.Posts.Api.Controllers;

internal class PostController : BaseController
{
    private readonly IPostService _postService;
    private readonly IPostLikeService _postLikeService;
    private readonly IShareService _shareService;
    private readonly IContext _context;

    public PostController(IPostService postService, IPostLikeService postLikeService, IShareService shareService, IContext context)
    {
        _postService = postService;
        _postLikeService = postLikeService;
        _shareService = shareService;
        _context = context;
    }

    [HttpGet("GetById/{id:guid}")]
    public async Task<ActionResult<PostDto?>> GetById(Guid id, CancellationToken cancellationToken = default)
        => OkOrNotFound(await _postService.GetAsync(id, cancellationToken));

    [HttpGet("GetAll")]
    public async Task<ActionResult<IReadOnlyList<PostDto>>> BrowseAsync(CancellationToken cancellationToken = default)
        => Ok(await _postService.BrowseAsync(_context.Id, cancellationToken));

    [HttpGet("GetAllPerCreator/{creatorId:guid}")]
    public async Task<ActionResult<IReadOnlyList<PostDto>>> BrowsePerCreatorAsync(Guid creatorId,
        CancellationToken cancellationToken = default) =>
        Ok(await _postService.BrowsePerCreatorAsync(creatorId, cancellationToken));
    
    [HttpGet("GetAllOwn")]
    public async Task<ActionResult<IReadOnlyList<PostDto>>> BrowseAllOwnAsync(CancellationToken cancellationToken = default)
        => Ok(await _postService.BrowseAllOwnAsync(_context.Id,cancellationToken));
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromForm]CreatePostCommand command, CancellationToken cancellationToken = default)
    {
        var id = await _postService.AddAsync(command,_context.Id, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromForm]UpdatePostCommand command, CancellationToken cancellationToken = default)
    {
        var id = await _postService.UpdateAsync(command,_context.Id, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAsync(DeletePostCommand command, CancellationToken cancellationToken = default)
    {
        await _postService.DeleteAsync(command,_context.Id, cancellationToken);
        return NoContent();
    }

    //likes
    [HttpPost("PostLike")]
    public async Task<ActionResult> AddPostLikeAsync(CreatePostLikeCommand command,
        CancellationToken cancellationToken = default)
    {
       var likesCount = await _postLikeService.AddAsync(command,_context.Id, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = command.Id }, likesCount);
    }

    [HttpDelete("PostLike")]
    public async Task<ActionResult> DeletePostLikeAsync(DeletePostLikeCommand command, CancellationToken cancellationToken = default)
    {
        await _postLikeService.DeleteAsync(command,_context.Id, cancellationToken);
        return NoContent();
    }

    [HttpGet("PostLike/{id:guid}")]
    public async Task<ActionResult<PostLikeDetailsDto>> GetPostLikeAsync(Guid id,
        CancellationToken cancellationToken = default) =>
        OkOrNotFound(await _postLikeService.GetAsync(id, cancellationToken));

    [HttpGet("PostLikes/{id:guid}")]
    public async Task<ActionResult<IReadOnlyList<PostLikeDto>>> BrowsePostLikesAsync(Guid id,
        CancellationToken cancellationToken = default) =>
        Ok(await _postLikeService.BrowseAsync(id, cancellationToken));

    //shares

    [HttpPost("Share")]
    public async Task<ActionResult> AddShareAsync(CreatePostShareCommand command,
        CancellationToken cancellationToken = default)
    {
        await _shareService.AddAsync(command,_context.Id, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = command.Id }, null);
    }

    [HttpDelete("Share")]
    public async Task<ActionResult> DeleteShareAsync(DeletePostShareCommand command, CancellationToken cancellationToken = default)
    {
        await _shareService.DeleteAsync(command,_context.Id, cancellationToken);
        return NoContent();
    }

    [HttpGet("Share/{id:guid}")]
    public async Task<ActionResult<ShareDetailsDto>> GetShareAsync(Guid id,
        CancellationToken cancellationToken = default) =>
        OkOrNotFound(await _shareService.GetByIdAsync(id, cancellationToken));

    [HttpGet("SharesPerPost/{id:guid}")]
    public async Task<ActionResult<IReadOnlyList<ShareDto>>> BrowseSharesAsync(Guid id,
        CancellationToken cancellationToken = default) =>
        Ok(await _shareService.BrowseSharesPerPostAsync(id, cancellationToken));
    
    [HttpGet("SharesPerUser/{id:guid}")]
    public async Task<ActionResult<IReadOnlyList<ShareDetailsDto>>> BrowseSharesPerUserAsync(Guid id,
        CancellationToken cancellationToken = default) =>
        Ok(await _shareService.BrowseSharesPerUserAsync(id, cancellationToken));
    
    
 
}