using MediatR;
using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Application.Educations.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Educations.Queries.GetAll;

public class GetAllUserEducationsQueryHandler : IRequestHandler<GetAllUserEducationsQuery, IReadOnlyCollection<GetEducationDto>>
{
    private readonly IEducationRepository _educationRepository;
    private static readonly EducationMapper Mapper = new();

    public GetAllUserEducationsQueryHandler(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }
    public async Task<IReadOnlyCollection<GetEducationDto>> Handle(GetAllUserEducationsQuery request, CancellationToken cancellationToken)
    {
        var educations = await _educationRepository.GetAllForConditionAsync(e => e.UserId == request.Id, cancellationToken);
        if (educations is null)
        {
            throw new NotFoundException("Educations not found");
        }
        return Mapper.GetEducationDtosToEducations(educations);
    }
}