using MediatR;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

public class UpdateUserBioCommandHandler : IRequestHandler<UpdateUserBioCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly UserMapper Mapper = new();

    public UpdateUserBioCommandHandler(IUserRepository userRepository,
        ICurrentUserContextService currentUserContextService)
    {
        _userRepository = userRepository;
        _currentUserContextService = currentUserContextService;
    }

    public async Task Handle(UpdateUserBioCommand request, CancellationToken cancellationToken)
    {
        var tokenId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        if (tokenId == Guid.Empty)
        {
            throw new NotFoundException("User not found");
        }

        var user = await _userRepository.GetByIdAsync(tokenId, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        var bioDto = new UserBioDto
        {
            Bio = request.Bio ?? user.Bio,
        };
        var updatedUserBio = Mapper.MapUpdateUserBioDtoToUser(user, bioDto);
        await _userRepository.UpdateAsync(updatedUserBio, cancellationToken);
    }
}