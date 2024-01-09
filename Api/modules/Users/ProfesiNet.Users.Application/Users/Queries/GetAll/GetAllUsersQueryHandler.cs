using MediatR;
using ProfesiNet.Shared.Contexts;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Queries.GetAll;

internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IReadOnlyCollection<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IContext _context;
    private static readonly UserMapper Mapper = new();

    public GetAllUsersQueryHandler(IUserRepository userRepository, IContext context)
    {
        _userRepository = userRepository;
        _context = context;
    }
    public async Task<IReadOnlyCollection<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var loggedUserId = _context.Id;
        var users = await _userRepository.GetAllAsync(cancellationToken);
        var dtos = Mapper.UserDtosToUsers(users);
        
        foreach (var dto in dtos)
        {
            dto.ProfilePicture = users.FirstOrDefault(x => x.Id == dto.Id)?.Photos.FirstOrDefault(x => x.PictureType == Domain.Enums.PictureType.ProfilePicture)?.Url;
            dto.Following = (bool)users.FirstOrDefault(x => x.Id == dto.Id)?.Followers.Contains(loggedUserId);
            dto.FollowedBy = (bool)users.FirstOrDefault(x => x.Id == dto.Id)?.Followings.Contains(loggedUserId);

        }
        return dtos;

        
    }
}

