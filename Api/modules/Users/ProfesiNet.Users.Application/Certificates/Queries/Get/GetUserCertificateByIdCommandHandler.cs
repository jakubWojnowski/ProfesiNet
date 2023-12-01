using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Certificates.Dtos;
using ProfesiNet.Users.Application.Certificates.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Certificates.Queries.Get;

internal class GetUserCertificateByIdCommandHandler : IRequestHandler<GetUserCertificateByIdCommand, GetCertificateDto>
{
    private readonly ICertificateRepository _certificateRepository;
    private readonly IUserRepository _userRepository;
    private static readonly CertificateMapper Mapper = new();

    public GetUserCertificateByIdCommandHandler(ICertificateRepository certificateRepository, IUserRepository userRepository)
    {
        _certificateRepository = certificateRepository;
        _userRepository = userRepository;
    }

    public async Task<GetCertificateDto> Handle(GetUserCertificateByIdCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.userId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.userId);
        }
        var certificate =
            await _certificateRepository.GetRecordByFilterAsync(c => c.Id == request.CertificateId && c.UserId == user.Id, cancellationToken);
        if (certificate == null)
        {
            throw new CertificateNotFoundException(request.CertificateId);
        }

        var certificateDto = Mapper.MapCertificateToGetCertificateDto(certificate);
        return certificateDto;
    }
}