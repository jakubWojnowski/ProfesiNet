using MediatR;
using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Application.Educations.Mappings;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Educations.Commands.Create;

public class CreateUserEducationCommandHandler : IRequestHandler<CreateUserEducationCommand, Guid>
{
    private readonly IEducationRepository _educationRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly EducationMapper Mapper = new();

    public CreateUserEducationCommandHandler(IEducationRepository educationRepository, ICurrentUserContextService currentUserContextService)
    {
        _educationRepository = educationRepository;
        _currentUserContextService = currentUserContextService;
    }
    public async Task<Guid> Handle(CreateUserEducationCommand request, CancellationToken cancellationToken)
    {
       var token = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
       if (token == Guid.Empty)
       {
           throw new NotFoundException("Token not found");
       }
       var educationDto = new EducationDto
       {
           Name = request.Name,
           Degree = request.Degree,
           FieldOfStudy = request.FieldOfStudy,
           StartDate = request.StartDate,
           EndDate = request.EndDate,
           Description = request.Description
       };
       var education = Mapper.MapEducationDtoToEducation(educationDto);
       education.UserId = token;
       
       var result = await _educationRepository.AddAsync(education, cancellationToken);

       return result;
    }
}