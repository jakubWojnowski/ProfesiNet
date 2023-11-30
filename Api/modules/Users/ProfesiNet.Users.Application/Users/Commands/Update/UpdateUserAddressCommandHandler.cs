using MediatR;
using ProfesiNet.Shared.Exceptions;
using ProfesiNet.Shared.UserContext;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Infrastructure.Repositories;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

public class UpdateUserAddressCommandHandler : IRequestHandler<UpdateUserAddressCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly UserMapper Mapper = new();

    public UpdateUserAddressCommandHandler(IUserRepository userRepository,
        ICurrentUserContextService currentUserContextService)
    {
        _userRepository = userRepository;
        _currentUserContextService = currentUserContextService;
    }

    public async Task Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
    {
        var tokenId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        var user = await _userRepository.GetByIdAsync(tokenId, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(tokenId);
        }
        
        var addressDto = new UserAddressDto
        {
            Address = request.Address ?? user.Address,
        };
        
        var updatedUserAddress = Mapper.MapUpdateUserAddressDtoToUser(user, addressDto);
        await _userRepository.UpdateAsync(updatedUserAddress, cancellationToken);
    }
}