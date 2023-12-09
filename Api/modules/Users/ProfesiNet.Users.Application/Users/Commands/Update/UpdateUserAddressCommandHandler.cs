using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal class UpdateUserAddressCommandHandler : IRequestHandler<UpdateUserAddressCommand>
{
    private readonly IUserRepository _userRepository;
    private static readonly UserMapper Mapper = new();

    public UpdateUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        
        var addressDto = new UserAddressDto
        {
            Address = request.Address ?? user.Address,
        };
        
        var updatedUserAddress = Mapper.MapUpdateUserAddressDtoToUser(user, addressDto);
        await _userRepository.UpdateAsync(updatedUserAddress, cancellationToken);
    }
}