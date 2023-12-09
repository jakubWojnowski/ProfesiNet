using MediatR;
using ProfesiNet.Users.Application.Policy;
using ProfesiNet.Users.Application.Skills.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Skills.Commands.Create;

internal class CreateSkillCommandHandler : IRequestHandler<CreateUserSkillCommand, Guid>
{
    private readonly ISkillRepository _skillRepository;
    private readonly ICannotAddSkillPolicy _cannotAddSkillPolicy;
    private readonly IUserRepository _userRepository;
    private static readonly SkillMapper Mapper = new();

    public CreateSkillCommandHandler(ISkillRepository skillRepository, ICannotAddSkillPolicy cannotAddSkillPolicy,
        IUserRepository userRepository)
    {
        _skillRepository = skillRepository;
        _cannotAddSkillPolicy = cannotAddSkillPolicy;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserSkillCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        if (!await _cannotAddSkillPolicy.CheckSkillsAsync(request.Name, request.UserId, cancellationToken))
        {
            throw new UserCannotAddSkillException(request.Name);
        }

        var skill = Mapper.MapSkillDtoToSkill(request);
        skill.UserID = user.Id;

        return await _skillRepository.AddAsync(skill, cancellationToken);
    }
}