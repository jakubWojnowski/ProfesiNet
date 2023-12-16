using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Queries.GetAll;

internal class GetAllUserFollowingsQueryHandler : IRequestHandler<GetAllUserFollowingsQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUserFollowingsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<UserDto>> Handle(GetAllUserFollowingsQuery request, CancellationToken cancellationToken)
    {
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
                    Bio = followed.Bio
                });
            }
        }

        return followings;
    }
}