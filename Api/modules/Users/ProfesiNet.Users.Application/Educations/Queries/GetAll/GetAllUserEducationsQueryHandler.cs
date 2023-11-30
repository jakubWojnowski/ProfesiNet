using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Application.Educations.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Educations.Queries.GetAll;

public class GetAllUserEducationsQueryHandler : IRequestHandler<GetAllUserEducationsQuery, IReadOnlyCollection<GetEducationDto>>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IUserRepository _userRepository;
    private static readonly EducationMapper Mapper = new();

    public GetAllUserEducationsQueryHandler(IEducationRepository educationRepository, IUserRepository userRepository)
    {
        _educationRepository = educationRepository;
        _userRepository = userRepository;
    }
    public async Task<IReadOnlyCollection<GetEducationDto>> Handle(GetAllUserEducationsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.Id, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }
        var educations = await _educationRepository.GetAllForConditionAsync(e => e.UserId == request.Id, cancellationToken);
        return Mapper.GetEducationDtosToEducations(educations);
    }
}