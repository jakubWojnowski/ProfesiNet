using MediatR;
using ProfesiNet.Users.Application.Certificates.Dtos;
using ProfesiNet.Users.Application.Certificates.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Certificates.Commands.Update;

internal class UpdateUserCertificateCommandHandler : IRequestHandler<UpdateUserCertificateCommand>
{
    private readonly ICertificateRepository _certificateRepository;
    private readonly IUserRepository _userRepository;
    private static readonly CertificateMapper Mapper = new();

    public UpdateUserCertificateCommandHandler( ICertificateRepository certificateRepository, IUserRepository userRepository)
    {
        _certificateRepository = certificateRepository;
        _userRepository = userRepository;
    }
    public async Task Handle(UpdateUserCertificateCommand request, CancellationToken cancellationToken)
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