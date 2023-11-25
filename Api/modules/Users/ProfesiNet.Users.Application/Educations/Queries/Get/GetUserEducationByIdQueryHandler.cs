using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Application.Educations.Mappings;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Educations.Queries.Get;

public class GetUserEducationByIdQueryHandler : IRequestHandler<GetUserEducationByIdQuery, GetEducationDto>
{
    private readonly IEducationRepository _educationRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly EducationMapper Mapper = new();

    public GetUserEducationByIdQueryHandler(IEducationRepository educationRepository, ICurrentUserContextService currentUserContextService)
    {
        _educationRepository = educationRepository;
        _currentUserContextService = currentUserContextService;
    }
    public async Task<GetEducationDto> Handle(GetUserEducationByIdQuery request, CancellationToken cancellationToken)
    {
        var education = await _educationRepository.GetRecordByFilterAsync(e => e.UserId == request.UserId && e.Id == request.EducationId, cancellationToken);
        if (education is null)
        {
            throw new NotFoundException("Education not found");
        }
        return Mapper.EducationToGetEducationDto(education);
    }
}