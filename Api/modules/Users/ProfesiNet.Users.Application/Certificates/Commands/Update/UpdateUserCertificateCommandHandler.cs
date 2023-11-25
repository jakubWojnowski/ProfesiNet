using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Certificates.Dtos;
using ProfesiNet.Users.Application.Certificates.Mappings;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Certificates.Commands.Update;

public class UpdateUserCertificateCommandHandler : IRequestHandler<UpdateUserCertificateCommand>
{
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly ICertificateRepository _certificateRepository;
    private static readonly CertificateMapper Mapper = new();

    public UpdateUserCertificateCommandHandler(ICurrentUserContextService currentUserContextService, ICertificateRepository certificateRepository)
    {
        _currentUserContextService = currentUserContextService;
        _certificateRepository = certificateRepository;
    }
    public async Task Handle(UpdateUserCertificateCommand request, CancellationToken cancellationToken)
    {
        var token = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        var certificate = await _certificateRepository.GetRecordByFilterAsync(c=> c.Id == request.Id && c.UserId == token, cancellationToken);
        if (certificate == null)
        {
            throw new NotFoundException("Certificate not found");
        }
        var certificateDto = new CertificateDto
        {
            Name = request.Name,
            Description = request.Description,
            Date = request.Date
        };
        
        var updatedCertificate = Mapper.MapUpdateCertificateDtoToCertificate(certificate, certificateDto);
        await _certificateRepository.UpdateAsync(updatedCertificate, cancellationToken);
        
    }
}