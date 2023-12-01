using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProfesiNet.Posts.Api.Controllers;
[ApiController]
[Route(BasePath + "/[controller]")]
[Authorize]
internal class BaseController : ControllerBase
{
    protected const string BasePath = "posts-module";
    
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }
}