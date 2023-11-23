using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Users.Application.Users.Commands.Login;
using ProfesiNet.Users.Application.Users.Commands.Logout;
using ProfesiNet.Users.Application.Users.Commands.Register;
using ProfesiNet.Users.Domain.Exceptions;
using ValidationException = FluentValidation.ValidationException;

namespace ProfesiNet.Users.Application.Controllers;

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
        try
        {
            var token = await _mediator.Send(command);
            return Ok(token);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutUserCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (UserAlreadyExistsException ex)
        {
            return StatusCode(StatusCodes.Status409Conflict, ex.Message);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}