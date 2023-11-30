using MediatR;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Experiences.Mappings;
using ProfesiNet.Users.Application.Policy;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Experiences.Commands.Create;

public class CreateUserExperienceCommandHandler : IRequestHandler<CreateUserExperienceCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IExperienceRepository _experienceRepository;
    private readonly ICannotSetDatePolicy _cannotSetDatePolicy;
    private static readonly ExperienceMapper Mapper = new();

    public CreateUserExperienceCommandHandler(IUserRepository userRepository, ICurrentUserContextService currentUserContextService, IExperienceRepository experienceRepository, ICannotSetDatePolicy cannotSetDatePolicy )
    {
        _userRepository = userRepository;
        _currentUserContextService = currentUserContextService;
        _experienceRepository = experienceRepository;
        _cannotSetDatePolicy = cannotSetDatePolicy;
    }
    public async Task<Guid> Handle(CreateUserExperienceCommand request, CancellationToken cancellationToken)
    {
        var tokenId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == tokenId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(tokenId);
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
        
        var experienceId =   await _experienceRepository.AddAsync(experience, cancellationToken);
         
         return experienceId;
    }
}