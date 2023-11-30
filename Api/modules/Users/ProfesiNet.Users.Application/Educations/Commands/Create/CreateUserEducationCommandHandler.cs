using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Application.Educations.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Educations.Commands.Create;

public class CreateUserEducationCommandHandler : IRequestHandler<CreateUserEducationCommand, Guid>
{
    private readonly IEducationRepository _educationRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IUserRepository _userRepository;
    private static readonly EducationMapper Mapper = new();

    public CreateUserEducationCommandHandler(IEducationRepository educationRepository, ICurrentUserContextService currentUserContextService, IUserRepository userRepository)
    {
        _educationRepository = educationRepository;
        _currentUserContextService = currentUserContextService;
        _userRepository = userRepository;
    }
    public async Task<Guid> Handle(CreateUserEducationCommand request, CancellationToken cancellationToken)
    {
       var token = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
       var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == token, cancellationToken);
         if (user is null)
         {
              throw new UserNotFoundException(token);
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
       education.UserId = user.Id;
       
       var educationId = await _educationRepository.AddAsync(education, cancellationToken);

       return educationId;
    }
}