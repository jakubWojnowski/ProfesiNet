using MediatR;
using ProfesiNet.Users.Application.Policy;
using ProfesiNet.Users.Application.Skills.Dtos;
using ProfesiNet.Users.Application.Skills.Mappings;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Skills.Commands.Create;

internal class CreateSkillCommandHandler : IRequestHandler<CreateUserSkillCommand>
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

    public async Task Handle(CreateUserSkillCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        foreach (var name in request.Names)
        {
            if (!await _cannotAddSkillPolicy.CheckSkillsAsync(name, request.UserId, cancellationToken))
            {
                throw new UserCannotAddSkillException(name);
            }
            var skill = Mapper.MapSkillDtoToSkill(new SkillDto
            {
                Name = name,
                Id = Guid.NewGuid()
            });
            skill.UserID = user.Id;
             await _skillRepository.AddAsync(skill, cancellationToken);
        }
        
    }
}