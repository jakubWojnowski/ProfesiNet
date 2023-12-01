using MediatR;

namespace ProfesiNet.Users.Application.Certificates.Commands.Delete;

internal record DeleteUserCertificateCommand(Guid Id) : IRequest;