using MediatR;
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
        var userId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        if (userId == Guid.Empty)
        {
            throw new NotFoundException("Token not Found");
        }
        var experience = await _experienceRepository.GetRecordByFilterAsync(e => e.UserId == userId && e.Id == request.Id, cancellationToken);
        if (experience is null)
        {
            throw new NotFoundException("Experience not found");
        }
        return Mapper.MapExperienceToGetExperienceDto(experience);
        
    }
}