using MediatR;
using ProfesiNet.Shared.Messaging;
using ProfesiNet.Users.Application.Events;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal class UpdateUserInformationCommandHandler : IRequestHandler<UpdateUserInformationCommand,UserDetailsDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageBroker _messageBroker;
    private static readonly UserMapper Mapper = new();

    public UpdateUserInformationCommandHandler(IUserRepository userRepository, IMessageBroker messageBroker)
    {
        _userRepository = userRepository;
        _messageBroker = messageBroker;
    }
    public async Task<UserDetailsDto> Handle(UpdateUserInformationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken) ?? throw new UserNotFoundException(request.UserId);

        user.Name = request.Name ?? user.Name;
        user.Surname = request.Surname ?? user.Surname;
        user.Title = request.Title ?? user.Title;
        user.Address = request.Address ?? user.Address;
        
       await _userRepository.UpdateAsync(user, cancellationToken);
        if (user.Name != request.Name || user.Surname != request.Surname || request.Name != null || request.Surname != null)
        {
            await _messageBroker.PublishMessageAsync(new UserFullNameUpdated(user.Id, user.Name, user.Surname));

        }
        var dto = Mapper.MapUserToUserDto(user);
        dto.ProfilePicture = user.Photos.FirstOrDefault(x => x.PictureType == Domain.Enums.PictureType.ProfilePicture)?.Url;
        return dto;
    }
}
