using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Users.Application.Users.Mappings;

[Mapper]
internal partial class UserMapper
{
    public partial User MapRegistrationDtoToUser(RegisterUserDto registerUserDto);
    
    public partial UserDto MapUserToUserDto(User user);
    
    public partial User MapUserDtoToUser(UserDto userDto);

    public  User MapUpdateUserBioDtoToUser(User user, UserBioDto userBio)
    {
        user.Bio = userBio.Bio;
        return user;
    }
    
    public User MapUpdateUser(User user, UserDto updateUserDto)
    {
        user.Name = updateUserDto.Name;
        user.Surname = updateUserDto.Surname;
        return user;
    }
    
    public User MapUpdateUserAddressDtoToUser(User user, UserAddressDto updateUserDto)
    {
        user.Address = updateUserDto.Address;
        return user;
    }

    public partial IReadOnlyCollection<UserDto> UserDtosToUsers(IEnumerable<User> users);
    public partial UserAndExperienceDto UserAndExperienceDtosToUsers(User users);
    
    
   


}