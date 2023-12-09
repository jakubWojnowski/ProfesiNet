using MediatR;
using ProfesiNet.Users.Application.Certificates.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Certificates.Commands.Delete;

internal class DeleteUserCertificateCommandHandler : IRequestHandler<DeleteUserCertificateCommand>
{
    private readonly ICertificateRepository _certificateRepository;
    private readonly IUserRepository _userRepository;
    private static readonly CertificateMapper Mapper = new();

    public DeleteUserCertificateCommandHandler(ICertificateRepository certificateRepository,IUserRepository userRepository)
    {
        _certificateRepository = certificateRepository;
        _userRepository = userRepository;
    }
    public async Task Handle(DeleteUserCertificateCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }
      
        var certificate = await _certificateRepository.GetRecordByFilterAsync(c=> c.Id == request.Id && c.UserId == user.Id, cancellationToken);
        if (certificate == null)
        {
            throw new CertificateNotFoundException(request.Id);
        }
        
        await _certificateRepository.DeleteAsync(certificate, cancellationToken);
        
    }
}