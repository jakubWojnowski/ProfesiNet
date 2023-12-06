using MediatR;
using ProfesiNet.Users.Application.Certificates.Dtos;

namespace ProfesiNet.Users.Application.Certificates.Queries.Get;

internal record GetCertificateByIdCommand(Guid CertificateId) : IRequest<GetCertificateDto>;