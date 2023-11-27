using MediatR;

namespace ProfesiNet.Users.Application.Certificates.Commands.Create;

public record CreateUserCertificateCommand(string Name, string? Description, DateOnly Date) : IRequest<Guid>;