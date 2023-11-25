using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Users.Application.Certificates.Mappings;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Certificates.Commands.Delete;

public class DeleteUserCertificateCommandHandler : IRequestHandler<DeleteUserCertificateCommand>
{
    private readonly ICertificateRepository _certificateRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly CertificateMapper Mapper = new();

    public DeleteUserCertificateCommandHandler(ICertificateRepository certificateRepository, ICurrentUserContextService currentUserContextService)
    {
        _certificateRepository = certificateRepository;
        _currentUserContextService = currentUserContextService;
    }
    public async Task Handle(DeleteUserCertificateCommand request, CancellationToken cancellationToken)
    {
        var token = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        if (token == Guid.Empty)
        {
            throw new NotFoundException("Token not found");
        }
        var certificate = await _certificateRepository.GetRecordByFilterAsync(c=> c.Id == request.Id && c.UserId == token, cancellationToken);
        if (certificate == null)
        {
            throw new NotFoundException("Certificate not found");
        }
        
        await _certificateRepository.DeleteAsync(certificate, cancellationToken);
        
    }
}