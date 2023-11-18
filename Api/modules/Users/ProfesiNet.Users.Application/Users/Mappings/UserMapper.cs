using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Users.Application.Users.Mappings;

[Mapper]
public partial class UserMapper
{
    public partial User MapRegistrationDtoToUser(RegisterDto registerDto);
    
    public partial User MapLoginDtoToUser(LoginDto loginDto);
}