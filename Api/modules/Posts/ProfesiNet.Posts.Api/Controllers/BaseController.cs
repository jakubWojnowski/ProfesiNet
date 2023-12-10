using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Posts.Api.Extension;

namespace ProfesiNet.Posts.Api.Controllers;

[Authorize]
[ApiController]
[Route(PostsModule.BasePath + "/[controller]")]
internal class BaseController : ControllerBase
{

    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}