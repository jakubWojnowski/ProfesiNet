using MediatR;
using ProfesiNet.Users.Application.Certificates.Dtos;
using ProfesiNet.Users.Application.Certificates.Mappings;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Certificates.Commands.Create;

public class CreateUserCertificateCommandHandler : IRequestHandler<CreateUserCertificateCommand, Guid>
{
    private readonly ICertificateRepository _certificateRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly CertificateMapper Mapper = new();

    public CreateUserCertificateCommandHandler(ICertificateRepository certificateRepository, ICurrentUserContextService currentUserContextService)
    {
        _certificateRepository = certificateRepository;
        _currentUserContextService = currentUserContextService;
    }
    public async Task<Guid> Handle(CreateUserCertificateCommand request, CancellationToken cancellationToken)
    {
        var token = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        if (token == Guid.Empty)
        {
            throw new NotFoundException("Token not found");
        }
        var certificateDto = new CertificateDto
        {
            Name = request.Name,
            Description = request.Description,
            Date = request.Date
        };
        var certificate = Mapper.MapCertificateDtoToCertificate(certificateDto);
        certificate.UserId = token;
        
        var certificationId = await _certificateRepository.AddAsync(certificate, cancellationToken);
        
        return certificationId;
        
    }
}