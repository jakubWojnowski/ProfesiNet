using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Application.Certificates.Dtos;
using ProfesiNet.Users.Application.Certificates.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Certificates.Commands.Create;

internal class CreateUserCertificateCommandHandler : IRequestHandler<CreateUserCertificateCommand, Guid>
{
    private readonly ICertificateRepository _certificateRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IUserRepository _userRepository;
    private static readonly CertificateMapper Mapper = new();

    public CreateUserCertificateCommandHandler(ICertificateRepository certificateRepository, ICurrentUserContextService currentUserContextService, IUserRepository userRepository)
    {
        _certificateRepository = certificateRepository;
        _currentUserContextService = currentUserContextService;
        _userRepository = userRepository;
    }
    public async Task<Guid> Handle(CreateUserCertificateCommand request, CancellationToken cancellationToken)
    {
        var tokenId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == tokenId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(tokenId);
        }
        var certificateDto = new CertificateDto
        {
            Name = request.Name,
            Description = request.Description,
            Date = request.Date
        };
        var certificate = Mapper.MapCertificateDtoToCertificate(certificateDto);
        certificate.UserId = user.Id;
        
        var certificationId = await _certificateRepository.AddAsync(certificate, cancellationToken);
        
        return certificationId;
        
    }
}