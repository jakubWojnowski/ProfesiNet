using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Users.Application.Certificates.Commands.Create;
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

namespace ProfesiNet.USers.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
            await _mediator.Send(new DeleteOwnAccountCommand());
            return NotFound();
    }
    
    [HttpPut("UpdateUserAddress")]
    public async Task<IActionResult> UpdateUserAddress([FromBody] UpdateUserAddressCommand command)
    {
            await _mediator.Send(command);
            return Ok(command.Address);

    }
    
    [HttpPut("UpdateUserBio")]
    public async Task<IActionResult> UpdateUserBio([FromBody] UpdateUserBioCommand command)
    {
            await _mediator.Send(command);
            return Ok(command.Bio);
    }
    [HttpGet("GetOwnProfile")]
    public async Task<IActionResult> GetOwnProfile()
    {
            var user = await _mediator.Send(new GetOwnProfileQuery());
            return Ok(user);
    }
    
    [HttpGet("GetUserById")]
    public async Task<IActionResult> GetUserById([FromQuery] GetUserByIdQuery query)
    {
            var user = await _mediator.Send(query);
            return Ok(user);
    }
    
    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
    }
    
    [HttpPost("CreateUserExperience")]
    public async Task<IActionResult> AddUserExperience([FromBody] CreateUserExperienceCommand command)
    {
           var id = await _mediator.Send(command);
            return Created($"api/AccountProfile/CreateUserExperience/{id}", id);
    }
    
    [HttpDelete("DeleteUserExperience")]
    public async Task<IActionResult> DeleteUserExperience([FromBody] DeleteUserExperienceCommand command)
    {
            await _mediator.Send(command);
            return NotFound();
    }

    [HttpPut("UpdateUserExperience")]
    public async Task<IActionResult> UpdateUserExperience([FromBody] UpdateUserExperienceCommand command)
    {
            await _mediator.Send(command);
            return Ok();
    }
    
    [HttpGet("GetUserExperienceById")]
    public async Task<IActionResult> GetUserExperienceById([FromQuery] GetUserExperienceByIdQuery query)
    {
            var experience = await _mediator.Send(query);
            return Ok(experience);
    }

    [HttpGet("GetAllUserExperience")]
    public async Task<IActionResult> GetAllUserExperience([FromQuery] GetAllUserExperienceQuery query)
    {
            var experiences = await _mediator.Send(query);
            return Ok(experiences);
    }
    
    [HttpGet("GetUserAndExperience")]
    public async Task<IActionResult> GetUserAndExperience([FromQuery] GetUserAndExperienceQuery query)
    {
            var user = await _mediator.Send(query);
            return Ok(user);
    }
    
    [HttpPost("CreateUserEducation")]
    public async Task<IActionResult> AddUserEducation([FromBody] CreateUserEducationCommand command)
    {
            var id = await _mediator.Send(command);
            return Created($"api/AccountProfile/CreateUserEducation/{id}", id);
    }
    
    [HttpDelete("DeleteUserEducation")]
    public async Task<IActionResult> DeleteUserEducation([FromBody] DeleteUserEducationCommand command)
    {
            await _mediator.Send(command);
            return Ok();
    }
    
    [HttpPut("UpdateUserEducation")]
    public async Task<IActionResult> UpdateUserEducation([FromBody] UpdateUserEducationCommand command)
    {
            await _mediator.Send(command);
            return Ok();
    }

    [HttpGet("GetUserEducationById")]
    public async Task<IActionResult> GetUserEducationById([FromQuery] GetUserEducationByIdQuery query)
    {
            var education = await _mediator.Send(query);
            return Ok(education);
    }

    [HttpGet("GetAllUserEducation")]
    public async Task<IActionResult> GetAllUserEducation([FromQuery] GetAllUserEducationsQuery query)
    {
            var educations = await _mediator.Send(query);
            return Ok(educations);
    }
    
    [HttpPost("CreateUserCertificate")]
    public async Task<IActionResult> AddUserCertification([FromBody] CreateUserCertificateCommand command)
    {
            var id = await _mediator.Send(command);
            return Created($"api/AccountProfile/CreateUserCertificate/{id}", id);
    }
}

//naprawic post certificate i education. daty sa zle