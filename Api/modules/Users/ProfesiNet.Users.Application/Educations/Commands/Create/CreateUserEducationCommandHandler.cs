using MediatR;
using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Application.Educations.Mappings;
using ProfesiNet.Users.Application.Policy;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Educations.Commands.Create;

internal class CreateUserEducationCommandHandler : IRequestHandler<CreateUserEducationCommand, Guid>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICannotSetDatePolicy _cannotSetDatePolicy;
    private static readonly EducationMapper Mapper = new();

    public CreateUserEducationCommandHandler(IEducationRepository educationRepository, IUserRepository userRepository,
        ICannotSetDatePolicy cannotSetDatePolicy)
    {
        _educationRepository = educationRepository;
        _userRepository = userRepository;
        _cannotSetDatePolicy = cannotSetDatePolicy;
    }
    public async Task<Guid> Handle(CreateUserEducationCommand request, CancellationToken cancellationToken)
    {
       var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.UserId, cancellationToken);
         if (user is null)
         {
              throw new UserNotFoundException(request.UserId);
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
       if (_cannotSetDatePolicy.IsSatisfiedBy(educationDto.StartDate, educationDto.EndDate) is false)
       {
           throw new CannotSetDateException(educationDto.StartDate, educationDto.EndDate); 
       }
       var education = Mapper.MapEducationDtoToEducation(educationDto);
       education.UserId = user.Id;
       
       var educationId = await _educationRepository.AddAsync(education, cancellationToken);

       return educationId;
    }
}