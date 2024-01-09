using MediatR;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Experiences.Mappings;
using ProfesiNet.Users.Application.Policy;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Experiences.Commands.Update;

internal class UpdateUserExperienceCommandHandler : IRequestHandler<UpdateUserExperienceCommand, Guid>
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICannotSetDatePolicy _cannotSetDatePolicy;
    private static readonly ExperienceMapper Mapper = new();

    public UpdateUserExperienceCommandHandler(IExperienceRepository experienceRepository,
        IUserRepository userRepository,
        ICannotSetDatePolicy cannotSetDatePolicy)
    {
        _experienceRepository = experienceRepository;
        _userRepository = userRepository;
        _cannotSetDatePolicy = cannotSetDatePolicy;
    }

    public async Task<Guid> Handle(UpdateUserExperienceCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var experience =
            await _experienceRepository.GetRecordByFilterAsync(e => e.Id == request.Id && e.UserId == user.Id,
                cancellationToken);

        if (experience is null)
        {
            throw new ExperienceNotFoundException(request.Id);
        }

        var experienceToUpdateDto = new ExperienceDto()
        {
            Company = request.Company ?? experience.Company,
            Position = request.Position ?? experience.Position,
            Description = request.Description ?? experience.Description,
            StartDate = request.StartDate ?? experience.StartDate,
            EndDate = request.EndDate ?? experience.EndDate
        };
        if (_cannotSetDatePolicy.IsSatisfiedBy(experienceToUpdateDto.StartDate, experienceToUpdateDto.EndDate) is false)
        {
            throw new CannotSetDateException(experienceToUpdateDto.StartDate, experienceToUpdateDto.EndDate);
        }

        var updatedExperience = Mapper.MapUpdateExperienceDtoToExperience(experience, experienceToUpdateDto);
        

         await _experienceRepository.UpdateAsync(updatedExperience, cancellationToken);
         
         return updatedExperience.Id;
    }
}