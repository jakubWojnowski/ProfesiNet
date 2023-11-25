using MediatR;
using ProfesiNet.Users.Application.Certificates.Dtos;
using ProfesiNet.Users.Application.Certificates.Mappings;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Certificates.Queries.GetAll;

public class GetAllUserCertificatesCommandHandler: IRequestHandler<GetAllUserCertificatesCommand, IReadOnlyCollection<GetCertificateDto>>
{
    private readonly ICertificateRepository _certificateRepository;
    private static readonly CertificateMapper Mapper = new();

    public GetAllUserCertificatesCommandHandler(ICertificateRepository certificateRepository)
    {
        _certificateRepository = certificateRepository;
    }
    public async Task<IReadOnlyCollection<GetCertificateDto>> Handle(GetAllUserCertificatesCommand request, CancellationToken cancellationToken)
    {
        var certificates = await _certificateRepository.GetAllForConditionAsync(c=> c.UserId == request.UserId, cancellationToken);
        var certificateDtos = Mapper.MapCertificatesToGetCertificateDto(certificates);
        return certificateDtos;
    }
}