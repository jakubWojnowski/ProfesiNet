using MediatR;
using ProfesiNet.Users.Application.Policy;
using ProfesiNet.Users.Application.Skills.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Skills.Commands.Update;

internal class UpdateUserSkillCommandHandler : IRequestHandler<UpdateUserSkillCommand>
{
    private readonly ISkillRepository _skillRepository;
    private readonly ICannotAddSkillPolicy _cannotAddSkillPolicy;
    private static readonly SkillMapper Mapper = new();

    public UpdateUserSkillCommandHandler(ISkillRepository skillRepository, ICannotAddSkillPolicy cannotAddSkillPolicy)
    {
        _skillRepository = skillRepository;
        _cannotAddSkillPolicy = cannotAddSkillPolicy;
    }
    public async Task Handle(UpdateUserSkillCommand request, CancellationToken cancellationToken)
    {

        var skill = await _skillRepository.GetRecordByFilterAsync(u => u.Id == request.Id && u.UserID == request.UserId, cancellationToken);
        if (skill is null)
        {
            throw new SkillNotFoundException(request.Id);
        }

        if (!await _cannotAddSkillPolicy.CheckSkillsAsync(request.Name, request.UserId, cancellationToken))
        {
            throw new UserCannotAddSkillException(request.Name);
        }
        
        var updatedSkill = Mapper.MapUpdateSkillCommandToSkill(request);
        
        await _skillRepository.UpdateAsync(updatedSkill, cancellationToken);
    }
}