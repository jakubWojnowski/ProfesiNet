using ProfesiNet.Shared.Exceptions;

namespace ProfesiNet.Users.Domain.Exceptions;

public class CertificateNotFoundException(Guid id) : ProfesiNetException($"Certificate with id {id} not found")
{
    public Guid Id { get; } = id;
}
