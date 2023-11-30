using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Educations.Commands.Delete;

public class DeleteUserEducationCommandHandler : IRequestHandler<DeleteUserEducationCommand>
{
    private readonly IEducationRepository _educationRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private readonly IUserRepository _userRepository;

    public DeleteUserEducationCommandHandler(IEducationRepository educationRepository, ICurrentUserContextService currentUserContextService, IUserRepository userRepository)
    {
        _educationRepository = educationRepository;
        _currentUserContextService = currentUserContextService;
        _userRepository = userRepository;
    }
    public async Task Handle(DeleteUserEducationCommand request, CancellationToken cancellationToken)
    {
        var tokenId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
       var user = await _userRepository.GetRecordByFilterAsync(u => u.Id == tokenId, cancellationToken);
         if (user is null)
         {
              throw new UserNotFoundException(tokenId);
         }
        var education = await _educationRepository.GetRecordByFilterAsync(e => e.Id == request.Id && e.UserId == user.Id, cancellationToken);
        
        if (education == null)
        {
            throw new EducationNotFoundException(request.Id);
        }
        
    
        await _educationRepository.DeleteAsync(education, cancellationToken);
        
    }
}