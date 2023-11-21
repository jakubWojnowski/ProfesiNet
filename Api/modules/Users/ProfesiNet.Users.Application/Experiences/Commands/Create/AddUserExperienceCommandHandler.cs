using MediatR;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Experiences.Mappings;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Experiences.Commands.Create;

public class AddUserExperienceCommandHandler : IRequestHandler<AddUserExperienceCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IExperienceRepository _experienceRepository;
    private static readonly ExperienceMapper Mapper = new();

    public AddUserExperienceCommandHandler(IUserRepository userRepository, ICurrentUserContextService currentUserContextService, IExperienceRepository experienceRepository)
    {
        _userRepository = userRepository;
        _currentUserContextService = currentUserContextService;
        _experienceRepository = experienceRepository;
    }
    public async Task<Guid> Handle(AddUserExperienceCommand request, CancellationToken cancellationToken)
    {
        var tokeId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        if (tokeId == Guid.Empty)
        {
            throw new NotFoundException("Token not Found");
        }
        
        var user = await _userRepository.GetByIdAsync(tokeId, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var experienceDto = new ExperienceDto
        {
            Company = request.Company,
            Position = request.Position,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate ?? null
        };

        var experience = Mapper.MapAddExperienceDtoToExperience(experienceDto);
        experience.UserId = user.Id;
        
        var result =   await _experienceRepository.AddAsync(experience, cancellationToken);
         
         return result;
    }
}