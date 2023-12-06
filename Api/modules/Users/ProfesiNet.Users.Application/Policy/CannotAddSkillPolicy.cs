using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Policy;

internal class CannotAddSkillPolicy : ICannotAddSkillPolicy
{
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly ISkillRepository _skillRepository;

    public CannotAddSkillPolicy(ICurrentUserContextService currentUserContextService, ISkillRepository skillRepository)
    {
        _currentUserContextService = currentUserContextService;
        _skillRepository = skillRepository;
    }
    
    public async Task<bool> CheckSkillsAsync(string name, CancellationToken ct = default )   
    {
        var token = Guid.Parse(_currentUserContextService.GetCurrentUser()!.Id!);
        var skill = await _skillRepository.GetRecordByFilterAsync(u => u.Id == token && u.Name == name, ct);
        return skill == null;
    }
}