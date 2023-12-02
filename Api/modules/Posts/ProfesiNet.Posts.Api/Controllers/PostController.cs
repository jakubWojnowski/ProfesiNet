using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Services;

namespace ProfesiNet.Posts.Api.Controllers;

internal class PostController : BaseController
{
    private readonly IPostService _postService;
    private readonly IPostLikeService _postLikeService;
    private readonly IShareService _shareService;

    public PostController(IPostService postService, IPostLikeService postLikeService, IShareService shareService)
    {
        _postService = postService;
        _postLikeService = postLikeService;
        _shareService = shareService;
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PostDto?>> Get(Guid id, CancellationToken cancellationToken = default) =>
        OkOrNotFound(await _postService.GetAsync(id, cancellationToken));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PostDto>>>
        BrowseAsync(CancellationToken cancellationToken = default) =>
        Ok(await _postService.BrowseAsync(cancellationToken));

    [HttpPost]
    public async Task<ActionResult> AddAsync(PostDto dto, CancellationToken cancellationToken = default)
    {
        await _postService.AddAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync(UpdatePostDto dto, CancellationToken cancellationToken = default)
    {
        await _postService.UpdateAsync(dto, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _postService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    //likes
    [HttpPost("PostLike")]
    public async Task<ActionResult> AddPostLikeAsync(PostLikeDetailsDto dto,
        CancellationToken cancellationToken = default)
    {
        await _postLikeService.AddAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);
    }

    [HttpDelete("PostLike/{id:guid}")]
    public async Task<ActionResult> DeletePostLikeAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _postLikeService.DeleteAsync(id, cancellationToken);
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
    public async Task<ActionResult> AddShareAsync(ShareDetailsDto dto,
        CancellationToken cancellationToken = default)
    {
        await _shareService.AddAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);
    }
    
    [HttpDelete("Share/{id:guid}")]
    public async Task<ActionResult> DeleteShareAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _shareService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
    
    [HttpGet("Share/{id:guid}")]
    public async Task<ActionResult<ShareDetailsDto>> GetShareAsync(Guid id,
        CancellationToken cancellationToken = default) =>
        OkOrNotFound(await _shareService.GetByIdAsync(id, cancellationToken));
    
    [HttpGet("Shares/{id:guid}")]
    public async Task<ActionResult<IReadOnlyList<ShareDto>>> BrowseSharesAsync(Guid id,
        CancellationToken cancellationToken = default) =>
        Ok(await _shareService.BrowseAsync(id, cancellationToken));
    
    
    
}