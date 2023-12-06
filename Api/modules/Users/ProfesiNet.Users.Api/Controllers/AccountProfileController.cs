using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Users.Application.Certificates.Commands.Create;
using ProfesiNet.Users.Application.Certificates.Commands.Delete;
using ProfesiNet.Users.Application.Certificates.Commands.Update;
using ProfesiNet.Users.Application.Certificates.Queries.Get;
using ProfesiNet.Users.Application.Certificates.Queries.GetAll;
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
using ProfesiNet.Users.Application.Skills.Commands.Create;
using ProfesiNet.Users.Application.Skills.Commands.Delete;
using ProfesiNet.Users.Application.Skills.Commands.Update;
using ProfesiNet.Users.Application.Skills.Queries.Get;
using ProfesiNet.Users.Application.Skills.Queries.GetAll;
using ProfesiNet.Users.Application.Users.Commands.Delete;
using ProfesiNet.Users.Application.Users.Commands.Update;
using ProfesiNet.Users.Application.Users.Queries.Get;
using ProfesiNet.Users.Application.Users.Queries.GetAll;

namespace ProfesiNet.Users.Api.Controllers;

internal class AccountProfileController : BaseController
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
    [HttpPatch("UpdateUserFullName")]
    public async Task<IActionResult> UpdateUserFullName(UpdateUserFullNameCommand command)
    {
        await _mediator.Send(command);
        return Ok(command.Name + " " + command.Surname);
    }
    

    [HttpPatch("UpdateUserAddress")]
    public async Task<IActionResult> UpdateUserAddress(UpdateUserAddressCommand command)
    {
        await _mediator.Send(command);
        return Ok(command.Address);
    }

    [HttpPatch("UpdateUserBio")]
    public async Task<IActionResult> UpdateUserBio(UpdateUserBioCommand command)
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
    public async Task<IActionResult> GetUserById(GetUserByIdQuery query)
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
    public async Task<IActionResult> AddUserExperience(CreateUserExperienceCommand command)
    {
        var id = await _mediator.Send(command);
        return Created($"api/AccountProfile/CreateUserExperience/{id}", id);
    }

    [HttpDelete("DeleteUserExperience")]
    public async Task<IActionResult> DeleteUserExperience(DeleteUserExperienceCommand command)
    {
        await _mediator.Send(command);
        return NotFound();
    }

    [HttpPut("UpdateUserExperience")]
    public async Task<IActionResult> UpdateUserExperience(UpdateUserExperienceCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("GetUserExperienceById")]
    public async Task<IActionResult> GetUserExperienceById(GetExperienceByIdQuery query)
    {
        var experience = await _mediator.Send(query);
        return Ok(experience);
    }

    [HttpGet("GetAllUserExperience")]
    public async Task<IActionResult> GetAllUserExperience(GetAllUserExperienceQuery query)
    {
        var experiences = await _mediator.Send(query);
        return Ok(experiences);
    }

    [HttpGet("GetUserAndExperience")]
    public async Task<IActionResult> GetUserAndExperience(GetUserAndExperienceQuery query)
    {
        var user = await _mediator.Send(query);
        return Ok(user);
    }

    [HttpPost("CreateUserEducation")]
    public async Task<IActionResult> AddUserEducation(CreateUserEducationCommand command)
    {
        var id = await _mediator.Send(command);
        return Created($"api/AccountProfile/CreateUserEducation/{id}", id);
    }

    [HttpDelete("DeleteUserEducation")]
    public async Task<IActionResult> DeleteUserEducation(DeleteUserEducationCommand command)
    {
        await _mediator.Send(command);
        return NotFound();
    }

    [HttpPut("UpdateUserEducation")]
    public async Task<IActionResult> UpdateUserEducation(UpdateUserEducationCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("GetUserEducationById")]
    public async Task<IActionResult> GetUserEducationById(GetEducationByIdQuery query)
    {
        var education = await _mediator.Send(query);
        return Ok(education);
    }

    [HttpGet("GetAllUserEducation")]
    public async Task<IActionResult> GetAllUserEducation(GetAllUserEducationsQuery query)
    {
        var educations = await _mediator.Send(query);
        return Ok(educations);
    }

    [HttpPost("CreateUserCertificate")]
    public async Task<IActionResult> AddUserCertification(CreateUserCertificateCommand command)
    {
        var id = await _mediator.Send(command);
        return Created($"api/AccountProfile/CreateUserCertificate/{id}", id);
    }

    [HttpPost("UpdateUserCertificate")]
    public async Task<IActionResult> UpdateUserCertificate(UpdateUserCertificateCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("DeleteUserCertificate")]
    public async Task<IActionResult> DeleteUserCertificate(DeleteUserCertificateCommand command)
    {
        await _mediator.Send(command);
        return NotFound();
    }

    [HttpGet("GetUserCertificateById")]
    public async Task<IActionResult> GetUserCertificateById(GetCertificateByIdCommand query)
    {
        var certificate = await _mediator.Send(query);
        return Ok(certificate);
    }

    [HttpGet("GetAllUserCertificates")]
    public async Task<IActionResult> GetAllUserCertificates(GetAllUserCertificatesQuery query)
    {
        var certificates = await _mediator.Send(query);
        return Ok(certificates);
    }
    
    [HttpPost("CreateUserSkill")]
    public async Task<IActionResult> AddUserSkill(CreateUserSkillCommand command)
    {
        var id = await _mediator.Send(command);
        return Created($"api/AccountProfile/CreateUserSkill/{id}", id);
    }
    
    [HttpDelete("DeleteUserSkill")]
    public async Task<IActionResult> DeleteUserSkill(DeleteUserSkillCommand command)
    {
        await _mediator.Send(command);
        return NotFound();
    }
    
    [HttpPut("UpdateUserSkill")]
    public async Task<IActionResult> UpdateUserSkill(UpdateUserSkillCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpGet("GetSkillById")]
    public async Task<IActionResult> GetUserSkillById(GetSkillByIdQuery query)
    {
        var skill = await _mediator.Send(query);
        return Ok(skill);
    }
    
    [HttpGet("GetAllUserSkills")]
    public async Task<IActionResult> GetAllUserSkills(GetAllSkillsPerUserQuery query)
    {
        var skills = await _mediator.Send(query);
        return Ok(skills);
    }
    
}

//naprawic post certificate i education. daty sa zle