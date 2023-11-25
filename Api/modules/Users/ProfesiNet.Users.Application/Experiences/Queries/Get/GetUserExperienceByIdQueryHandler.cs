using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Experiences.Mappings;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Experiences.Queries.Get;

public class GetUserExperienceByIdQueryHandler : IRequestHandler<GetUserExperienceByIdQuery, GetExperienceDto>
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly ExperienceMapper Mapper = new();

    public GetUserExperienceByIdQueryHandler(IExperienceRepository experienceRepository, ICurrentUserContextService currentUserContextService)
    {
        _experienceRepository = experienceRepository;
        _currentUserContextService = currentUserContextService;
    }
    public async Task<GetExperienceDto> Handle(GetUserExperienceByIdQuery request, CancellationToken cancellationToken)
    {
        var experience = await _experienceRepository.GetRecordByFilterAsync(e => e.UserId == request.UserId && e.Id == request.ExperienceId, cancellationToken);
        if (experience is null)
        {
            throw new NotFoundException("Experience not found");
        }
        return Mapper.MapExperienceToGetExperienceDto(experience);
        
    }
}