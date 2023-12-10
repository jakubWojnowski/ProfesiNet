using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Users.Application.Users.Commands.Login;
using ProfesiNet.Users.Application.Users.Commands.Register;

namespace ProfesiNet.Users.Api.Controllers;

internal class UserAuthenticationController : BaseController
{
    private readonly IMediator _mediator;

    public UserAuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login( LoginUserCommand command)
    {
            var token = await _mediator.Send(command);
            return Ok(token);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
            await _mediator.Send(command);
            return Created("/api/user", null);
    }
}