using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Queries.Get;

internal class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, ProfileDto>
{
    private readonly IUserRepository _userRepository;
    private static readonly UserMapper Mapper = new();

    public GetUserProfileByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ProfileDto> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var profile = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (profile is null)
        {
            throw new UserNotFoundException(request.Id);
        }
        var dto = Mapper.MapUserToProfileDto(profile);
        dto.ProfilePicture = profile.Photos.FirstOrDefault(x => x.PictureType == Domain.Enums.PictureType.ProfilePicture)?.Url;
        
        return dto;
    }
}