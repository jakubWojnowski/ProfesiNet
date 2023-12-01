using MediatR;
using ProfesiNet.Users.Application.Certificates.Dtos;
using ProfesiNet.Users.Application.Certificates.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Certificates.Queries.GetAll;

internal class GetAllUserCertificatesCommandHandler: IRequestHandler<GetAllUserCertificatesCommand, IReadOnlyCollection<GetCertificateDto>>
{
    public IUserRepository UserRepository { get; }
    private readonly ICertificateRepository _certificateRepository;
    private static readonly CertificateMapper Mapper = new();

    public GetAllUserCertificatesCommandHandler(ICertificateRepository certificateRepository, IUserRepository userRepository)
    {
        UserRepository = userRepository;
        _certificateRepository = certificateRepository;
    }
    public async Task<IReadOnlyCollection<GetCertificateDto>> Handle(GetAllUserCertificatesCommand request, CancellationToken cancellationToken)
    {
        var user = await UserRepository.GetRecordByFilterAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        var certificates = await _certificateRepository.GetAllForConditionAsync(c=> c.UserId == user.Id, cancellationToken);
        var certificateDtos = Mapper.MapCertificatesToGetCertificateDto(certificates);
        return certificateDtos;
    }
}