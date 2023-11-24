﻿using MediatR;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Experiences.Mappings;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Experiences.Queries.GetAll;

public class
    GetAllUserExperienceQueryHandler : IRequestHandler<GetAllUserExperienceQuery, IEnumerable<GetExperienceDto>>
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly ExperienceMapper Mapper = new();

    public GetAllUserExperienceQueryHandler(IExperienceRepository experienceRepository,
        ICurrentUserContextService currentUserContextService)
    {
        _experienceRepository = experienceRepository;
        _currentUserContextService = currentUserContextService;
    }

    public async Task<IEnumerable<GetExperienceDto>> Handle(GetAllUserExperienceQuery request,
        CancellationToken cancellationToken)
    {
        var userId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        if (userId == Guid.Empty)
        {
            throw new NotFoundException("Token not Found");
        }

        var experiences =
            await _experienceRepository.GetAllForConditionAsync(e => e.UserId == userId, cancellationToken);
        if (experiences is null)
        {
            throw new NotFoundException("Experience not found");
        }

        return Mapper.MapExperiencesToGetExperienceDtos(experiences);
    }
}