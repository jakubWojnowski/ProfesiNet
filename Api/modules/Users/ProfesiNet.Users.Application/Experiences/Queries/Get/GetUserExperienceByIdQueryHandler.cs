using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Experiences.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Experiences.Queries.Get;

public class GetUserExperienceByIdQueryHandler : IRequestHandler<GetUserExperienceByIdQuery, GetExperienceDto>
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IUserRepository _userRepository;
    private static readonly ExperienceMapper Mapper = new();

    public GetUserExperienceByIdQueryHandler(IExperienceRepository experienceRepository, ICurrentUserContextService currentUserContextService, IUserRepository userRepository)
    {
        _experienceRepository = experienceRepository;
        _currentUserContextService = currentUserContextService;
        _userRepository = userRepository;
    }
    public async Task<GetExperienceDto> Handle(GetUserExperienceByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        var experience = await _experienceRepository.GetRecordByFilterAsync(e => e.UserId == user.Id && e.Id == request.ExperienceId, cancellationToken);
        if (experience is null)
        {
            throw new ExperienceNotFoundException(request.ExperienceId);
        }
        return Mapper.MapExperienceToGetExperienceDto(experience);
        
    }
}