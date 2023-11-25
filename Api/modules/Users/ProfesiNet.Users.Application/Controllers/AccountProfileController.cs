using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Users.Application.Educations.Commands.Create;
using ProfesiNet.Users.Application.Educations.Commands.Delete;
using ProfesiNet.Users.Application.Educations.Commands.Update;
using ProfesiNet.Users.Application.Educations.Queries.Get;
using ProfesiNet.Users.Application.Educations.Queries.GetAll;
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
    [HttpDelete("DeleteOwnAccount")]
    public async Task<IActionResult> DeleteOwnAccount()
    {
        try
        {
            await _mediator.Send(new DeleteOwnAccountCommand());
            return NotFound();
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
    
    [HttpPost("CreateUserExperience")]
    public async Task<IActionResult> AddUserExperience([FromBody] CreateUserExperienceCommand command)
    {
        try
        {
           var id = await _mediator.Send(command);
            return Created($"api/AccountProfile/CreateUserExperience/{id}", id);
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
            return NotFound();
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
    public async Task<IActionResult> GetAllUserExperience([FromQuery] GetAllUserExperienceQuery query)
    {
        try
        {
            var experiences = await _mediator.Send(query);
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
    
    [HttpGet("GetUserAndExperience")]
    public async Task<IActionResult> GetUserAndExperience([FromQuery] GetUserAndExperienceQuery query)
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
    
    [HttpPost("CreateUserEducation")]
    public async Task<IActionResult> AddUserEducation([FromBody] CreateUserEducationCommand command)
    {
        try
        {
            var id = await _mediator.Send(command);
            return Created($"api/AccountProfile/CreateUserEducation/{id}", id);
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
    
    [HttpDelete("DeleteUserEducation")]
    public async Task<IActionResult> DeleteUserEducation([FromBody] DeleteUserEducationCommand command)
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
    
    [HttpPut("UpdateUserEducation")]
    public async Task<IActionResult> UpdateUserEducation([FromBody] UpdateUserEducationCommand command)
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
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("GetUserEducationById")]
    public async Task<IActionResult> GetUserEducationById([FromQuery] GetUserEducationByIdQuery query)
    {
        try
        {
            var education = await _mediator.Send(query);
            return Ok(education);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
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

    [HttpGet("GetAllUserEducation")]
    public async Task<IActionResult> GetAllUserEducation([FromQuery] GetAllUserEducationsQuery query)
    {
        try
        {
            var educations = await _mediator.Send(query);
            return Ok(educations);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
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