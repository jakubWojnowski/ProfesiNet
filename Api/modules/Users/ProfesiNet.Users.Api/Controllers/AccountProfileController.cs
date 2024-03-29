﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfesiNet.Shared.Contexts;
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
using ProfesiNet.Users.Application.Photos.Commands.Create;
using ProfesiNet.Users.Application.Photos.Commands.Delete;
using ProfesiNet.Users.Application.Skills.Commands.Create;
using ProfesiNet.Users.Application.Skills.Commands.Delete;
using ProfesiNet.Users.Application.Skills.Commands.Update;
using ProfesiNet.Users.Application.Skills.Queries.GetAll;
using ProfesiNet.Users.Application.Users.Commands.Delete;
using ProfesiNet.Users.Application.Users.Commands.Update;
using ProfesiNet.Users.Application.Users.Queries.Get;
using ProfesiNet.Users.Application.Users.Queries.GetAll;

namespace ProfesiNet.Users.Api.Controllers;

[Authorize]
internal class AccountProfileController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IContext _context;


    public AccountProfileController(IMediator mediator, IContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpDelete("DeleteOwnAccount")]
    public async Task<IActionResult> DeleteOwnAccount()
    {
        await _mediator.Send(new DeleteOwnAccountCommand(_context.Id));
        return NotFound();
    }

    [HttpPatch("UpdateUserFullName")]
    public async Task<IActionResult> UpdateUserFullName(UpdateUserFullNameCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok(command.Name + " " + command.Surname);
    }


    [HttpPatch("UpdateUserAddress")]
    public async Task<IActionResult> UpdateUserAddress(UpdateUserAddressCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok(command.Address);
    }

    [HttpPatch("UpdateUserBio")]
    public async Task<IActionResult> UpdateUserBio(UpdateUserBioCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok(command.Bio);
    }

    [HttpGet("GetOwnProfile")]
    public async Task<IActionResult> GetOwnProfile()
    {
        var user = await _mediator.Send(new GetOwnProfileQuery(_context.Id));
        return Ok(user);
    }
    [HttpGet("GetUserProfileById/{userId:guid}")]
    public async Task<IActionResult> GetUserProfileById(Guid userId)
    {
        var user = await _mediator.Send(new GetUserProfileByIdQuery(userId));
        return Ok(user);
    }

    [HttpGet("GetUserById/{userId:guid}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(userId));
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
        var id = await _mediator.Send(command with { UserId = _context.Id });
        return Created($"api/AccountProfile/CreateUserExperience/{id}", id);
    }

    [HttpDelete("DeleteUserExperience")]
    public async Task<IActionResult> DeleteUserExperience(DeleteUserExperienceCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok();
    }

    [HttpPut("UpdateUserExperience")]
    public async Task<IActionResult> UpdateUserExperience(UpdateUserExperienceCommand command)
    {
      var id =  await _mediator.Send(command with { UserId = _context.Id });
        return Ok(id);
    }

    [HttpGet("GetUserExperienceById/{id:guid}")]
    public async Task<IActionResult> GetUserExperienceById(Guid id)
    {
        var experience = await _mediator.Send(new GetExperienceByIdQuery(id));
        return Ok(experience);
    }

    [HttpGet("GetAllUserExperience/{userId:guid}")]
    public async Task<IActionResult> GetAllUserExperience(Guid userId)
    {
        var experiences = await _mediator.Send(new GetAllUserExperienceQuery(userId));
        return Ok(experiences);
    }

    [HttpGet("GetUserAndExperience{userId:guid}")]
    public async Task<IActionResult> GetUserAndExperience(Guid userId)
    {
        var user = await _mediator.Send(new GetUserAndExperienceQuery(userId));
        return Ok(user);
    }

    [HttpPost("CreateUserEducation")]
    public async Task<IActionResult> AddUserEducation(CreateUserEducationCommand command)
    {
        var id = await _mediator.Send(command with { UserId = _context.Id });
        return Created($"api/AccountProfile/CreateUserEducation/{id}", id);
    }

    [HttpDelete("DeleteUserEducation")]
    public async Task<IActionResult> DeleteUserEducation(DeleteUserEducationCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok();
    }

    [HttpPut("UpdateUserEducation")]
    public async Task<IActionResult> UpdateUserEducation(UpdateUserEducationCommand command)
    {
       var id = await _mediator.Send(command with { UserId = _context.Id });
        return Ok(id);
    }

    [HttpGet("GetUserEducationById/{id:guid}")]
    public async Task<IActionResult> GetUserEducationById(Guid id)
    {
        var education = await _mediator.Send(new GetUserEducationByIdQuery(id));
        return Ok(education);
    }

    [HttpGet("GetAllUserEducation/{userId:guid}")]
    public async Task<IActionResult> GetAllUserEducation(Guid userId)
    {
        var educations = await _mediator.Send(new GetAllUserEducationsQuery(userId));
        return Ok(educations);
    }

    [HttpPost("CreateUserCertificate")]
    public async Task<IActionResult> AddUserCertification(CreateUserCertificateCommand command)
    {
        var id = await _mediator.Send(command with { UserId = _context.Id });
        return Created($"api/AccountProfile/CreateUserCertificate/{id}", id);
    }

    [HttpPost("UpdateUserCertificate")]
    public async Task<IActionResult> UpdateUserCertificate(UpdateUserCertificateCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok();
    }

    [HttpDelete("DeleteUserCertificate")]
    public async Task<IActionResult> DeleteUserCertificate(DeleteUserCertificateCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return NotFound();
    }

    [HttpGet("GetUserCertificateById/{id:guid}")]
    public async Task<IActionResult> GetUserCertificateById(Guid id)
    {
        var certificate = await _mediator.Send(new GetCertificateByIdCommand(id));
        return Ok(certificate);
    }

    [HttpGet("GetAllUserCertificates/{userId:guid}")]
    public async Task<IActionResult> GetAllUserCertificates(Guid userId)
    {
        var certificates = await _mediator.Send(new GetAllUserCertificatesQuery(userId));
        return Ok(certificates);
    }

    [HttpPost("CreateUserSkill")]
    public async Task<IActionResult> AddUserSkill(CreateUserSkillCommand command)
    {
        var ids = await _mediator.Send(command with { UserId = _context.Id });
        return Created($"api/AccountProfile/CreateUserSkill/{ids}", ids);
    }

    [HttpDelete("DeleteUserSkill")]
    public async Task<IActionResult> DeleteUserSkill(DeleteUserSkillCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok();
    }

    [HttpPut("UpdateUserSkill")]
    public async Task<IActionResult> UpdateUserSkill(UpdateUserSkillCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok();
    }

    [HttpGet("GetSkillById/{id:guid}")]
    public async Task<IActionResult> GetUserSkillById(Guid id)
    {
        var skill = await _mediator.Send(new GetExperienceByIdQuery(id));
        return Ok(skill);
    }

    [HttpGet("GetAllUserSkills/{userId:guid}")]
    public async Task<IActionResult> GetAllUserSkills(Guid userId)
    {
        var skills = await _mediator.Send(new GetAllSkillsPerUserQuery(userId));
        return Ok(skills);
    }


    [HttpPatch("UpdateUserFollowings")]
    public async Task<IActionResult> UpdateUserFollowings(UpdateUserFollowingsCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok();
    }

    [HttpPatch("UpdateUserConnectionInvitations")]
    public async Task<IActionResult> UpdateUserConnectionInvitations(UpdateConnectionInvitationCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok();
    }

    [HttpPatch("UpdateUserConnections")]
    public async Task<IActionResult> UpdateUserConnections(UpdateConnectionCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok();
    }

    [HttpPatch("RemoveUserConnection")]
    public async Task<IActionResult> RemoveUserConnection(DeleteConnectionCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok();
    }

    [HttpPatch("RemoveUserConnectionReceivedInvitation")]
    public async Task<IActionResult> RemoveUserConnectionInvitation(
        DeleteConnectionInvitationReceivedCommand receivedCommand)
    {
        await _mediator.Send(receivedCommand with { UserId = _context.Id });
        return Ok();
    }

    [HttpPatch("RemoveUserConnectionSentInvitation")]
    public async Task<IActionResult> RemoveUserConnectionSentInvitation(
        DeleteConnectionInvitationSentCommand sentCommand)
    {
        await _mediator.Send(sentCommand with { UserId = _context.Id });
        return Ok();
    }

    [HttpPatch("RemoveUserFollowing")]
    public async Task<IActionResult> RemoveUserFollowing(DeleteFollowingCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return Ok();
    }

    [HttpGet("GetAllUserFollowings/{userId:guid}")]
    public async Task<IActionResult> GetAllUserFollowings(Guid userId)
    {
        var followings = await _mediator.Send(new GetAllUserFollowingsQuery(userId));
        return Ok(followings);
    }

    [HttpGet("GetAllUserFollowers/{userId:guid}")]
    public async Task<IActionResult> GetAllUserFollowers(Guid userId)
    {
        var followers = await _mediator.Send(new GetAllUserFollowersQuery(userId));
        return Ok(followers);
    }

    [HttpGet("GetAllUserConnections/{userId:guid}")]
    public async Task<IActionResult> GetAllUserConnections(Guid userId)
    {
        var connections = await _mediator.Send(new GetAllUserConnectionsQuery(userId));
        return Ok(connections);
    }

    [HttpGet("GetAllUserConnectionInvitationsReceived/{userId:guid}")]
    public async Task<IActionResult> GetAllUserConnectionInvitations(Guid userId)
    {
        var invitations = await _mediator.Send(new GetAllUserReceivedInvitationsQuery(userId));
        return Ok(invitations);
    }

    [HttpGet("GetAllUserReceivedInvitationsSent/{userId:guid}")]
    public async Task<IActionResult> GetAllUserReceivedInvitationsSent(Guid userId)
    {
        var invitations = await _mediator.Send(new GetAllUserReceivedInvitationsSentQuery(userId));
        return Ok(invitations);
    }

    [HttpPost("AddUserProfilePicture")]
    public async Task<IActionResult> AddUserProfilePicture([FromForm] AddUserProfilePictureCommand command)
    {
        var url = await _mediator.Send(command with { UserId = _context.Id });
        return Ok(url);
    }

    [HttpDelete("DeleteUserProfilePicture")]
    public async Task<IActionResult> DeleteUserProfilePicture(DeleteUserProfilePictureCommand command)
    {
        await _mediator.Send(command with { UserId = _context.Id });
        return NoContent();
    }

    [HttpPatch("UpdateUserInformation")]
    public async Task<IActionResult> UpdateUserInformation(UpdateUserInformationCommand command)
    {
        var user = await _mediator.Send(command with { UserId = _context.Id });
        return Ok(user);
    }
}