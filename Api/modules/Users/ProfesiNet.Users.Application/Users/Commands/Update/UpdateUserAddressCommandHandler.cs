using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Application.Users.Services.UserContext;
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
        var tokeId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        if (tokeId == Guid.Empty)
        {
            throw new NotFoundException("id in token not found");
        }

        var user = await _userRepository.GetByIdAsync(tokeId, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var addressDto = new UserAddressDto
        {
            Address = request.Address,
        };
        
        var updatedUserAddress = Mapper.MapUpdateUserAddressDtoToUser(user, addressDto);
        await _userRepository.UpdateAsync(updatedUserAddress, cancellationToken);
    }
}