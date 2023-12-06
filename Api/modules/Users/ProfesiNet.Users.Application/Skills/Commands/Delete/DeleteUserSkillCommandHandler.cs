using MediatR;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Skills.Commands.Delete;

internal class DeleteUserSkillCommandHandler : IRequestHandler<DeleteUserSkillCommand>
{
    private readonly ISkillRepository _skillRepository;
    private readonly ICurrentUserContextService _currentUserContextService;

    public DeleteUserSkillCommandHandler(ISkillRepository skillRepository, ICurrentUserContextService currentUserContextService)
    {
        _skillRepository = skillRepository;
        _currentUserContextService = currentUserContextService;
    }

    public async Task Handle(DeleteUserSkillCommand request, CancellationToken cancellationToken)
    {
        var token = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);

        var skill = await _skillRepository.GetRecordByFilterAsync(u => u.Id == request.Id && u.UserID == token, cancellationToken);
        if (skill is null)
        {
            throw new SkillNotFoundException(request.Id);
        }
        
        await _skillRepository.DeleteAsync(skill, cancellationToken);
    }
}