using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Policy;

internal class CannotAddSkillPolicy : ICannotAddSkillPolicy
{
    private readonly ISkillRepository _skillRepository;

    public CannotAddSkillPolicy(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<bool> CheckSkillsAsync(string name, Guid userId, CancellationToken ct = default)
    {
        var skill = await _skillRepository.GetRecordByFilterAsync(u => u.UserId == userId && u.Name == name, ct);
        return skill == null;
    }
}