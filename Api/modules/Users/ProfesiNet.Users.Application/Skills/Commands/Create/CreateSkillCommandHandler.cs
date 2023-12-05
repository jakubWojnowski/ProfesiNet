using MediatR;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Application.Skills.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Skills.Commands.Create;

internal class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, Guid>
{
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly ISkillRepository _skillRepository;
    private static readonly SkillMapper Mapper = new();

    public CreateSkillCommandHandler(ICurrentUserContextService currentUserContextService,
        ISkillRepository skillRepository)
    {
        _currentUserContextService = currentUserContextService;
        _skillRepository = skillRepository;
    }

    public async Task<Guid> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var token = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);

        var user = await _skillRepository.GetRecordByFilterAsync(u => u.Id == token, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(token);
        }

        var skill = Mapper.MapSkillDtoToSkill(request with
        {
            Id = Guid.NewGuid(),
        });
        skill.UserID = user.Id;

        return await _skillRepository.AddAsync(skill, cancellationToken);
    }
}