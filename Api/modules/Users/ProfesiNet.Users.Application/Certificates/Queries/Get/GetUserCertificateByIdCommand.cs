using MediatR;
using ProfesiNet.Users.Application.Certificates.Dtos;

namespace ProfesiNet.Users.Application.Certificates.Queries.Get;

public record GetUserCertificateByIdCommand(Guid Id) : IRequest<GetCertificateDto>;