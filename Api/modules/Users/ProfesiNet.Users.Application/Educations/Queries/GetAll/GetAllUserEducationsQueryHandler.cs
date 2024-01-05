using Confab.Shared.Abstractions.Interfaces;
using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Application.Educations.Mappings;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Educations.Queries.GetAll;

internal class GetAllUserEducationsQueryHandler : IRequestHandler<GetAllUserEducationsQuery, IReadOnlyCollection<EducationDto>>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IClock _clock;
    private static readonly EducationMapper Mapper = new();

    public GetAllUserEducationsQueryHandler(IEducationRepository educationRepository, IUserRepository userRepository, IClock clock)
    {
        _educationRepository = educationRepository;
        _userRepository = userRepository;
        _clock = clock;
    }
    public async Task<IReadOnlyCollection<EducationDto>> Handle(GetAllUserEducationsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.Id, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }
        var educations = await _educationRepository.GetAllForConditionAsync(e => e.UserId == request.Id, cancellationToken);
        var enumerable = educations as Education?[] ?? educations.ToArray();
        var filteredEducations = enumerable.AsQueryable().OrderByDescending(e => e.StartDate).ToList();
        return Mapper.MapEducationDtosToEducations(filteredEducations);
    }
}