using MediatR;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Skills.Commands.Delete;

internal class DeleteUserSkillCommandHandler : IRequestHandler<DeleteUserSkillCommand>
{
    private readonly ISkillRepository _skillRepository;

    public DeleteUserSkillCommandHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task Handle(DeleteUserSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetRecordByFilterAsync(u => u.Id == request.Id && u.UserId == request.UserId, cancellationToken);
        if (skill is null)
        {
            throw new SkillNotFoundException(request.Id);
        }
        
        await _skillRepository.DeleteAsync(skill, cancellationToken);
    }
}