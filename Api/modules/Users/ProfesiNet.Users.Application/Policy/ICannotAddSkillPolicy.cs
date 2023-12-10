namespace ProfesiNet.Users.Application.Policy;

internal interface ICannotAddSkillPolicy
{
    Task<bool> CheckSkillsAsync(string name,Guid userId, CancellationToken ct = default );
}