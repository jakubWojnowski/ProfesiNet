namespace ProfesiNet.Users.Application.Policy;

internal interface ICannotAddSkillPolicy
{
    Task<bool> CheckSkillsAsync(string name, CancellationToken ct = default );
}