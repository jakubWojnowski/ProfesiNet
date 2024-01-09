using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Experiences.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Experiences.Queries.GetAll;

internal class GetAllUserExperienceQueryHandler : IRequestHandler<GetAllUserExperienceQuery, IEnumerable<GetExperienceDto>>
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly IUserRepository _userRepository;
    private static readonly ExperienceMapper Mapper = new();

    public GetAllUserExperienceQueryHandler(IExperienceRepository experienceRepository
       ,IUserRepository userRepository)
    {
        _experienceRepository = experienceRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<GetExperienceDto>> Handle(GetAllUserExperienceQuery request, CancellationToken cancellationToken) 
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.Id, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }
        var experiences = await _experienceRepository.GetAllForConditionAsync(e => e.UserId == user.Id, cancellationToken);
        var enumerable = experiences.ToList();
        var filteredExperiences = enumerable.AsQueryable().OrderByDescending(e => e.StartDate).ToList();
        return Mapper.MapExperiencesToGetExperienceDtos(filteredExperiences);
    }
}