using MediatR;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Educations.Commands.Delete;

public class DeleteUserEducationCommandHandler : IRequestHandler<DeleteUserEducationCommand>
{
    private readonly IEducationRepository _educationRepository;
    private readonly ICurrentUserContextService _currentUserContextService;

    public DeleteUserEducationCommandHandler(IEducationRepository educationRepository, ICurrentUserContextService currentUserContextService)
    {
        _educationRepository = educationRepository;
        _currentUserContextService = currentUserContextService;
    }
    public async Task Handle(DeleteUserEducationCommand request, CancellationToken cancellationToken)
    {
        var tokenId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        if (tokenId == Guid.Empty )
        {
            throw new NotFoundException("Token id not Found");
        }
        var education = await _educationRepository.GetRecordByFilterAsync(e => e.Id == request.Id && e.UserId == tokenId, cancellationToken);
        
        if (education == null)
        {
            throw new NotFoundException("Education not found");
        }
        
    
        await _educationRepository.DeleteAsync(education, cancellationToken);
        
    }
}