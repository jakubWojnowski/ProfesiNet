using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class EducationNotFoundException (Guid id) : ProfesiNetException($"Education with id {id} not found")
{
    public Guid Id { get; } = id;
}
