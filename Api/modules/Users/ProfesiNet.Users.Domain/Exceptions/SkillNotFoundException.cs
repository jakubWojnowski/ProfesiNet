using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class SkillNotFoundException(Guid id) : ProfesiNetException($"Skill with id {id} not found");
