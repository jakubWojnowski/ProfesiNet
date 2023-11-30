using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Experiences.Commands.Delete;

public class DeleteUserExperienceCommandHandler : IRequestHandler<DeleteUserExperienceCommand>
{
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IExperienceRepository _experienceRepository;
    private readonly IUserRepository _userRepository;

    public DeleteUserExperienceCommandHandler(ICurrentUserContextService currentUserContextService,
        IExperienceRepository experienceRepository, IUserRepository userRepository)
    {
        _currentUserContextService = currentUserContextService;
        _experienceRepository = experienceRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserExperienceCommand request, CancellationToken cancellationToken)
    {
        var tokenId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == tokenId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(tokenId);
        }
        var experience = await _experienceRepository.GetRecordByFilterAsync(e => e.Id == request.Id && e.UserId == user.Id, cancellationToken);
        if (experience == null)
        {
            throw new ExperienceNotFoundException(request.Id);
        }
        
        await _experienceRepository.DeleteAsync(experience, cancellationToken);
    }
}