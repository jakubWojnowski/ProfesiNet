using MediatR;

namespace ProfesiNet.Users.Application.Certificates.Commands.Delete;

public record DeleteUserCertificateCommand(Guid Id) : IRequest;