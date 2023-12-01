using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Posts.Core.Dto;
using ProfesiNet.Posts.Core.Interfaces;

namespace ProfesiNet.Posts.Api.Controllers;

internal class PostController : BaseController
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PostDto?>> Get(Guid id) => OkOrNotFound(await _postService.GetAsync(id));
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PostDto>>> BrowseAsync() => Ok(await _postService.BrowseAsync());
    
    [HttpPost]
    public async Task<ActionResult> AddAsync(PostDto dto)
    {
        await _postService.AddAsync(dto);
        return CreatedAtAction(nameof(Get), new {id = dto.Id}, null);
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateAsync( UpdatePostDto dto)
    {
      
        await _postService.UpdateAsync(dto);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _postService.DeleteAsync(id);
        return NoContent();
    }
}