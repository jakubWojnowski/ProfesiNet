using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Queries.Get;

internal class GetOwnProfileQueryHandler : IRequestHandler<GetOwnProfileQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    private static readonly UserMapper Mapper = new();

    public GetOwnProfileQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(GetOwnProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var dto = Mapper.MapUserToUserDto(user);
        dto.ProfilePicture = user.Photos.FirstOrDefault(x => x.PictureType == Domain.Enums.PictureType.ProfilePicture)?.Url;
        return dto;
    }
}