using MediatR;

namespace ProfesiNet.Users.Application.Certificates.Commands.Create;

internal record CreateUserCertificateCommand(string Name, string? Description, DateTime Date) : IRequest<Guid>;