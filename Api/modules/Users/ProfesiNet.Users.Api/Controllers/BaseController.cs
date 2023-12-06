using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Users.Api.Extension;

namespace ProfesiNet.Users.Api.Controllers;


[ApiController]
[Route(UsersModule.BasePath + "/[controller]")]
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