using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class UserNotFoundException(Guid id) : ProfesiNetException($"User with id {id} not found")
{
    public Guid Id { get; } = id;
}
