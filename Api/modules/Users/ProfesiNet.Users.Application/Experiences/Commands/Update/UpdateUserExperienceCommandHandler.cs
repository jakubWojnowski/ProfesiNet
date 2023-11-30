using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Experiences.Mappings;
using ProfesiNet.Users.Application.Policy;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Experiences.Commands.Update;

public class UpdateUserExperienceCommandHandler : IRequestHandler<UpdateUserExperienceCommand>
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IUserRepository _userRepository;
    private readonly ICannotSetDatePolicy _cannotSetDatePolicy;
    private static readonly ExperienceMapper Mapper = new();

    public UpdateUserExperienceCommandHandler(IExperienceRepository experienceRepository, ICurrentUserContextService currentUserContextService, IUserRepository userRepository,
        ICannotSetDatePolicy cannotSetDatePolicy)
    {
        _experienceRepository = experienceRepository;
        _currentUserContextService = currentUserContextService;
        _userRepository = userRepository;
        _cannotSetDatePolicy = cannotSetDatePolicy;
    }
    public async Task Handle(UpdateUserExperienceCommand request, CancellationToken cancellationToken)
    {
        var tokenId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == tokenId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(tokenId);
        }
        var experience = await _experienceRepository.GetRecordByFilterAsync(e => e.Id == request.Id && e.UserId == user.Id, cancellationToken);

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
    }
}