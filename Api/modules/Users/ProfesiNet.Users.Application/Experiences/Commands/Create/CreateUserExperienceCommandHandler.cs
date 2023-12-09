using MediatR;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Experiences.Mappings;
using ProfesiNet.Users.Application.Policy;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Experiences.Commands.Create;

internal class CreateUserExperienceCommandHandler : IRequestHandler<CreateUserExperienceCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IExperienceRepository _experienceRepository;
    private readonly ICannotSetDatePolicy _cannotSetDatePolicy;
    private static readonly ExperienceMapper Mapper = new();

    public CreateUserExperienceCommandHandler(IUserRepository userRepository,
        IExperienceRepository experienceRepository, ICannotSetDatePolicy cannotSetDatePolicy)
    {
        _userRepository = userRepository;
        _experienceRepository = experienceRepository;
        _cannotSetDatePolicy = cannotSetDatePolicy;
    }

    public async Task<Guid> Handle(CreateUserExperienceCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }


        var experienceDto = new ExperienceDto
        {
            Company = request.Company,
            Position = request.Position,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate ?? null
        };
        if (_cannotSetDatePolicy.IsSatisfiedBy(experienceDto.StartDate, experienceDto.EndDate) is false)
        {
            throw new CannotSetDateException(experienceDto.StartDate, experienceDto.EndDate);
        }

        var experience = Mapper.MapAddExperienceDtoToExperience(experienceDto);
        experience.UserId = user.Id;

        var experienceId = await _experienceRepository.AddAsync(experience, cancellationToken);

        return experienceId;
    }
}