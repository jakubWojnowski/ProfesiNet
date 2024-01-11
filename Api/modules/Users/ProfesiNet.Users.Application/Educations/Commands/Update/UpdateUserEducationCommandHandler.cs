using MediatR;
using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Application.Educations.Mappings;
using ProfesiNet.Users.Application.Policy;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Educations.Commands.Update;

internal class UpdateUserEducationCommandHandler : IRequestHandler<UpdateUserEducationCommand, Guid>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICannotSetDatePolicy _cannotSetDatePolicy;
    private static readonly EducationMapper Mapper = new();

    public UpdateUserEducationCommandHandler(IEducationRepository educationRepository, IUserRepository userRepository,
        ICannotSetDatePolicy cannotSetDatePolicy)
    {
        _educationRepository = educationRepository;
        _userRepository = userRepository;
        _cannotSetDatePolicy = cannotSetDatePolicy;
    }

    public async Task<Guid> Handle(UpdateUserEducationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var education =
            await _educationRepository.GetRecordByFilterAsync(e => e.Id == request.Id && e.UserId == user.Id,
                cancellationToken);
        if (education == null)
        {
            throw new EducationNotFoundException(request.Id);
        }

        var educationToUpdateDto = new EducationDto()
        {
            Name = request.Name ?? education.Name,
            Address = request.Address ?? education.Address,
            Degree = request.Degree ?? education.Degree,
            FieldOfStudy = request.FieldOfStudy ?? education.FieldOfStudy,
            StartDate = request.StartDate ?? education.StartDate,
            EndDate = request.EndDate ?? null
        };
        if (_cannotSetDatePolicy.IsSatisfiedBy(educationToUpdateDto.StartDate, educationToUpdateDto.EndDate) is false)
        {
            throw new CannotSetDateException(educationToUpdateDto.StartDate, educationToUpdateDto.EndDate);
        }

        var updatedEducation = Mapper.MapUpdateEducationDtoToEducation(education, educationToUpdateDto);
        await _educationRepository.UpdateAsync(updatedEducation, cancellationToken);
        return updatedEducation.Id;
    }
}