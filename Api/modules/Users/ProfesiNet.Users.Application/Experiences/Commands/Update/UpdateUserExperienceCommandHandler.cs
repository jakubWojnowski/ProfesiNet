using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Experiences.Mappings;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Experiences.Commands.Update;

public class UpdateUserExperienceCommandHandler : IRequestHandler<UpdateUserExperienceCommand>
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly ExperienceMapper Mapper = new();

    public UpdateUserExperienceCommandHandler(IExperienceRepository experienceRepository, ICurrentUserContextService currentUserContextService)
    {
        _experienceRepository = experienceRepository;
        _currentUserContextService = currentUserContextService;
    }
    public async Task Handle(UpdateUserExperienceCommand request, CancellationToken cancellationToken)
    {
        var tokenId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        var experience = await _experienceRepository.GetRecordByFilterAsync(e => e.Id == request.Id && e.UserId == tokenId, cancellationToken);

        if (experience is null)
        {
            throw new NotFoundException("Experience not found");
        }
        var experienceToUpdateDto = new ExperienceDto()
        {
            Company = request.Company ?? experience.Company,
            Position = request.Position ?? experience.Position,
            Description = request.Description ?? experience.Description,
            StartDate = request.StartDate ?? experience.StartDate,
            EndDate = request.EndDate ?? experience.EndDate
        };
        
        var updatedExperience = Mapper.MapUpdateExperienceDtoToExperience(experience, experienceToUpdateDto);
        
        await _experienceRepository.UpdateAsync(updatedExperience, cancellationToken);
    }
}