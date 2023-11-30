using MediatR;

namespace ProfesiNet.Users.Application.Certificates.Commands.Update;

public record UpdateUserCertificateCommand(Guid Id, string Name, string? Description, DateTime Date) : IRequest;