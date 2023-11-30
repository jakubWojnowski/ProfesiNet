using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Users.Application.Users.Commands.Login;
using ProfesiNet.Users.Application.Users.Commands.Logout;
using ProfesiNet.Users.Application.Users.Commands.Register;

namespace ProfesiNet.USers.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserAuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
            var token = await _mediator.Send(command);
            return Ok(token);
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutUserCommand command)
    {
            await _mediator.Send(command);
            return Redirect("/");
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
            await _mediator.Send(command);
            return Created("/api/user", null);
    }
}