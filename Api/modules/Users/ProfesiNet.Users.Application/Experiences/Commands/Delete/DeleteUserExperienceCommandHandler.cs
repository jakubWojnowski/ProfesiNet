using MediatR;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Experiences.Commands.Delete;

public class DeleteUserExperienceCommandHandler : IRequestHandler<DeleteUserExperienceCommand>
{
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IExperienceRepository _experienceRepository;

    public DeleteUserExperienceCommandHandler(ICurrentUserContextService currentUserContextService,
        IExperienceRepository experienceRepository)
    {
        _currentUserContextService = currentUserContextService;
        _experienceRepository = experienceRepository;
    }

    public async Task Handle(DeleteUserExperienceCommand request, CancellationToken cancellationToken)
    {
        var tokenId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        var experience = await _experienceRepository.GetByIdAsync(request.Id, cancellationToken);
        if (experience == null)
        {
            throw new NotFoundException("Experience not found");
        }

        if (tokenId == Guid.Empty || experience.UserId != tokenId)
        {
            throw new NotFoundException("Token not Found or incorrect user");
        }
        
        await _experienceRepository.DeleteAsync(experience, cancellationToken);
    }
}