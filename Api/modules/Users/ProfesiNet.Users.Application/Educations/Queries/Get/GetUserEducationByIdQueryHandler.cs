using MediatR;
using Microsoft.Extensions.Logging;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Application.Educations.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Educations.Queries.Get;

internal class GetUserEducationByIdQueryHandler : IRequestHandler<GetEducationByIdQuery, GetEducationDto>
{
    private readonly IEducationRepository _educationRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IUserRepository _userRepository;
    private static readonly EducationMapper Mapper = new();

    public GetUserEducationByIdQueryHandler(IEducationRepository educationRepository, ICurrentUserContextService currentUserContextService, IUserRepository userRepository)
    {
        _educationRepository = educationRepository;
        _currentUserContextService = currentUserContextService;
        _userRepository = userRepository;
    }
    public async Task<GetEducationDto> Handle(GetEducationByIdQuery request, CancellationToken cancellationToken)
    {
        var education = await _educationRepository.GetByIdAsync(request.EducationId, cancellationToken);
        if (education is null)
        {
            throw new EducationNotFoundException(request.EducationId);
        }
        return Mapper.EducationToGetEducationDto(education);
    }
}