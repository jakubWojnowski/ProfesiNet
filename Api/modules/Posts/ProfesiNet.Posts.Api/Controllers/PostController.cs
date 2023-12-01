using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Interfaces;

namespace ProfesiNet.Posts.Api.Controllers;

internal class PostController : BaseController
{
    private readonly IPostService _postService;
    private readonly IPostLikeService _postLikeService;

    public PostController(IPostService postService, IPostLikeService postLikeService)
    {
        _postService = postService;
        _postLikeService = postLikeService;
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
}