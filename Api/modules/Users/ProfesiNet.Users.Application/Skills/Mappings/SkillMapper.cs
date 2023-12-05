using ProfesiNet.Users.Application.Skills.Commands.Create;
using ProfesiNet.Users.Application.Skills.Dtos;
using ProfesiNet.Users.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Users.Application.Skills.Mappings;
[Mapper]
internal partial class SkillMapper
{
    public partial SkillDto MapSkillToSkillDto(Skill skill);
    public partial Skill MapSkillDtoToSkill(CreateSkillCommand createSkillCommand);

}