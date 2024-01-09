using MediatR;
using ProfesiNet.Shared.Contexts;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Domain.Enums;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Queries.GetAll;

internal class GetAllUserFollowingsQueryHandler : IRequestHandler<GetAllUserFollowingsQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IContext _context;

    public GetAllUserFollowingsQueryHandler(IUserRepository userRepository, IContext context)
    {
        _userRepository = userRepository;
        _context = context;
    }
    public async Task<IEnumerable<UserDto>> Handle(GetAllUserFollowingsQuery request, CancellationToken cancellationToken)
    {
        var loggedUserId = _context.Id;

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var followingGuids = user.Followings;
        var followings = new List<UserDto>();

        foreach (var id in followingGuids)
        {
            var followed = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (followed != null)
            {
                followings.Add(new UserDto
                {
                    Id = followed.Id,
                    Name = followed.Name,
                    Surname = followed.Surname,
                    Address = followed.Address,
                    ProfilePicture = followed.Photos.FirstOrDefault(x => x.PictureType == PictureType.ProfilePicture)?.Url,
                    Title = followed.Title,
                    Bio = followed.Bio,
                    Following = followed.Followers.Contains(loggedUserId),
                    FollowedBy = followed.Followings.Contains(loggedUserId)
                });
            }
        }

        return followings;
    }
}