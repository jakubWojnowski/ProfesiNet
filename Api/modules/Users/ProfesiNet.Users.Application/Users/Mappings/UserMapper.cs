using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Users.Commands.Update;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Users.Application.Users.Mappings;

[Mapper]
internal partial class UserMapper
{
    public partial User MapRegistrationDtoToUser(RegisterUserDto registerUserDto);

    
    public partial UserDetailsDto MapUserToUserDto(User user); 
    public partial UserLoggedInDto MapUserToUserLoggedInDto(User user); 

    public User MapUpdateUserBioDtoToUser(User user, UserBioDto userBio)
    {
        user.Bio = userBio.Bio;
        return user;
    }

    public User MapUpdateUserAddressDtoToUser(User user, UserAddressDto updateUserDto)
    {
        user.Address = updateUserDto.Address;
        return user;
    }

    public partial IReadOnlyCollection<UserDto> UserDtosToUsers(IEnumerable<User> users);
    public partial UserAndExperienceDto UserAndExperienceDtosToUsers(User users);
    

    public partial User MapUpdateUserFullNameCommandToUser(UpdateUserFullNameCommand command);
}