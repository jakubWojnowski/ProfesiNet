using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Certificates.Dtos;
using ProfesiNet.Users.Application.Certificates.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Certificates.Queries.Get;

internal class GetCertificateByIdCommandHandler : IRequestHandler<GetCertificateByIdCommand, GetCertificateDto>
{
    private readonly ICertificateRepository _certificateRepository;
    private readonly IUserRepository _userRepository;
    private static readonly CertificateMapper Mapper = new();

    public GetCertificateByIdCommandHandler(ICertificateRepository certificateRepository, IUserRepository userRepository)
    {
        _certificateRepository = certificateRepository;
        _userRepository = userRepository;
    }

    public async Task<GetCertificateDto> Handle(GetCertificateByIdCommand request,
        CancellationToken cancellationToken)
    {
        var certificate =
            await _certificateRepository.GetRecordByFilterAsync(c => c.Id == request.CertificateId, cancellationToken);
        if (certificate == null)
        {
            throw new CertificateNotFoundException(request.CertificateId);
        }

        var certificateDto = Mapper.MapCertificateToGetCertificateDto(certificate);
        return certificateDto;
    }
}