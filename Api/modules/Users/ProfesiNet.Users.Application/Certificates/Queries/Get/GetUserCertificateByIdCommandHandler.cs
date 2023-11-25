using MediatR;
using ProfesiNet.Users.Application.Certificates.Dtos;
using ProfesiNet.Users.Application.Certificates.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Certificates.Queries.Get;

public class GetUserCertificateByIdCommandHandler : IRequestHandler<GetUserCertificateByIdCommand, GetCertificateDto>
{
    private readonly ICertificateRepository _certificateRepository;
    private static readonly CertificateMapper Mapper = new();

    public GetUserCertificateByIdCommandHandler(ICertificateRepository certificateRepository)
    {
        _certificateRepository = certificateRepository;
    }

    public async Task<GetCertificateDto> Handle(GetUserCertificateByIdCommand request,
        CancellationToken cancellationToken)
    {
        var certificate =
            await _certificateRepository.GetRecordByFilterAsync(c => c.Id == request.Id, cancellationToken);
        if (certificate == null)
        {
            throw new NotFoundException("Certificate not found");
        }

        var certificateDto = Mapper.MapCertificateToGetCertificateDto(certificate);
        return certificateDto;
    }
}