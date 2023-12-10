using MediatR;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Experiences.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Experiences.Queries.Get;

internal class GetExperienceByIdQueryHandler : IRequestHandler<GetExperienceByIdQuery, GetExperienceDto>
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly IUserRepository _userRepository;
    private static readonly ExperienceMapper Mapper = new();

    public GetExperienceByIdQueryHandler(IExperienceRepository experienceRepository, IUserRepository userRepository)
    {
        _experienceRepository = experienceRepository;
        _userRepository = userRepository;
    }
    public async Task<GetExperienceDto> Handle(GetExperienceByIdQuery request, CancellationToken cancellationToken)
    {
        var experience = await _experienceRepository.GetByIdAsync(request.ExperienceId, cancellationToken);
        if (experience is null)
        {
            throw new ExperienceNotFoundException(request.ExperienceId);
        }
        return Mapper.MapExperienceToGetExperienceDto(experience);
        
    }
}