using MediatR;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Experiences.Mappings;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Experiences.Commands.Create;

public class CreateUserExperienceCommandHandler : IRequestHandler<CreateUserExperienceCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IExperienceRepository _experienceRepository;
    private static readonly ExperienceMapper Mapper = new();

    public CreateUserExperienceCommandHandler(IUserRepository userRepository, ICurrentUserContextService currentUserContextService, IExperienceRepository experienceRepository)
    {
        _userRepository = userRepository;
        _currentUserContextService = currentUserContextService;
        _experienceRepository = experienceRepository;
    }
    public async Task<Guid> Handle(CreateUserExperienceCommand request, CancellationToken cancellationToken)
    {
        var tokenId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        var experienceDto = new ExperienceDto
        {
            Company = request.Company,
            Position = request.Position,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate ?? null
        };

        var experience = Mapper.MapAddExperienceDtoToExperience(experienceDto);
        experience.UserId = tokenId;
        
        var experienceId =   await _experienceRepository.AddAsync(experience, cancellationToken);
         
         return experienceId;
    }
}