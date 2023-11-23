using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Users.Application.Experiences.Commands.Create;
using ProfesiNet.Users.Application.Experiences.Commands.Delete;
using ProfesiNet.Users.Application.Experiences.Commands.Update;
using ProfesiNet.Users.Application.Experiences.Queries.Get;
using ProfesiNet.Users.Application.Experiences.Queries.GetAll;
using ProfesiNet.Users.Application.Users.Commands.Delete;
using ProfesiNet.Users.Application.Users.Commands.Update;
using ProfesiNet.Users.Application.Users.Queries.Get;
using ProfesiNet.Users.Application.Users.Queries.GetAll;
using ProfesiNet.Users.Domain.Exceptions;

namespace ProfesiNet.Users.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand command)
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
    
    [HttpDelete("DeleteOwnAccount")]
    public async Task<IActionResult> DeleteOwnAccount()
    {
        try
        {
            await _mediator.Send(new DeleteOwnAccountCommand());
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
    
    [HttpPut("UpdateUserAddress")]
    public async Task<IActionResult> UpdateUserAddress([FromBody] UpdateUserAddressCommand command)
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
    
    [HttpPut("UpdateUserBio")]
    public async Task<IActionResult> UpdateUserBio([FromBody] UpdateUserBioCommand command)
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
    [HttpGet("GetOwnProfile")]
    public async Task<IActionResult> GetOwnProfile()
    {
        try
        {
            var user = await _mediator.Send(new GetOwnProfileQuery());
            return Ok(user);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById([FromQuery] GetUserByIdQuery query)
    {
        try
        {
            var user = await _mediator.Send(query);
            return Ok(user);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost("AddUserExperience")]
    public async Task<IActionResult> AddUserExperience([FromBody] AddUserExperienceCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpDelete("DeleteUserExperience")]
    public async Task<IActionResult> DeleteUserExperience([FromBody] DeleteUserExperienceCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("UpdateUserExperience")]
    public async Task<IActionResult> UpdateUserExperience([FromBody] UpdateUserExperienceCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet("GetUserExperienceById")]
    public async Task<IActionResult> GetUserExperienceById([FromQuery] GetUserExperienceByIdQuery query)
    {
        try
        {
            var experience = await _mediator.Send(query);
            return Ok(experience);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("GetAllUserExperience")]
    public async Task<IActionResult> GetAllUserExperience()
    {
        try
        {
            var experiences = await _mediator.Send(new GetAllUserExperienceQuery());
            return Ok(experiences);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}