using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class UserCannotAddSkillException(string name) : ProfesiNetException(
    $"User cannot add skill {name} because it already exists");