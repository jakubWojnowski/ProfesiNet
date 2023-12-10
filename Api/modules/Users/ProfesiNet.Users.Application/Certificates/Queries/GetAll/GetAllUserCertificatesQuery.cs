using MediatR;
using ProfesiNet.Users.Application.Certificates.Dtos;

namespace ProfesiNet.Users.Application.Certificates.Queries.GetAll;

internal record GetAllUserCertificatesQuery(Guid UserId) : IRequest<IReadOnlyCollection<GetCertificateDto>>;