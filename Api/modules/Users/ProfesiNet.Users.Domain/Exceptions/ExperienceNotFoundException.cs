using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class ExperienceNotFoundException(Guid id) : ProfesiNetException($"Experience with id {id} not found")
{
    public Guid Id { get; } = id;
}
