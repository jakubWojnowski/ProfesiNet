using MediatR;
using ProfesiNet.Shared.Commands;
using ProfesiNet.Users.Application.Policy;
using ProfesiNet.Users.Application.Skills.Mappings;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Skills.Commands.Create;

internal class CreateSkillCommandHandler : ICommandHandler<CreateUserSkillCommand>
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

    // public async Task<Guid> Handle(CreateUserSkillCommand request, CancellationToken cancellationToken)
    // {
    //     var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.UserId, cancellationToken);
    //     if (user is null)
    //     {
    //         throw new UserNotFoundException(request.UserId);
    //     }
    //
    //     if (!await _cannotAddSkillPolicy.CheckSkillsAsync(request.Name, request.UserId, cancellationToken))
    //     {
    //         throw new UserCannotAddSkillException(request.Name);
    //     }
    //
    //     var skill = Mapper.MapSkillDtoToSkill(request);
    //     skill.UserID = user.Id;
    //
    //     return await _skillRepository.AddAsync(skill, cancellationToken);
    // }

    public async Task HandleAsync(CreateUserSkillCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == command.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(command.UserId);
        }

        foreach (var name in command.Names)
        {
            if (!await _cannotAddSkillPolicy.CheckSkillsAsync(name, command.UserId, cancellationToken))
            {
                throw new UserCannotAddSkillException(name);
            }
            var skill = new Skill
            {
                Id = Guid.NewGuid(), // Zakładając, że nadal chcesz generować nowy identyfikator dla każdej umiejętności
                Name = name,
                UserID = command.UserId
            };

            await _skillRepository.AddAsync(skill, cancellationToken);
            
            
        }
        
    }
}