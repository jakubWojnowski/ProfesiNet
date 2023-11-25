using ProfesiNet.Users.Application.Certificates.Dtos;
using ProfesiNet.Users.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Users.Application.Certificates.Mappings;
[Mapper]
public partial class CertificateMapper
{

    public partial Certificate MapCertificateDtoToCertificate(CertificateDto certificateDto);
    
    public Certificate MapUpdateCertificateDtoToCertificate(Certificate certificate, CertificateDto certificateDto)
    {
        certificate.Name = certificateDto.Name;
        certificate.Description = certificateDto.Description;
        certificate.Date = certificateDto.Date;
        return certificate;
        
    }
    
    public partial GetCertificateDto MapCertificateToGetCertificateDto(Certificate certificate);
    public partial IReadOnlyCollection<GetCertificateDto> MapCertificatesToGetCertificateDto(IEnumerable<Certificate?> certificate);
}