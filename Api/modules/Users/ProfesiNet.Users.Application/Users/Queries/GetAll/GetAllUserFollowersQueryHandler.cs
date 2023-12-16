using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Queries.GetAll;

internal class GetAllUserFollowersQueryHandler : IRequestHandler<GetAllUserFollowersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private static readonly UserMapper Mapper = new();

    public GetAllUserFollowersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<UserDto>> Handle(GetAllUserFollowersQuery request, CancellationToken cancellationToken)
    {
        
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var followersGuids = user.Followers;
        var followers = new List<UserDto>();

        foreach (var id in followersGuids)
        {
            var follower = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (follower != null)
            {
                followers.Add(new UserDto
                {
                    Id = follower.Id,
                    Name = follower.Name,
                    Surname = follower.Surname,
                    Address = follower.Address,
                    Bio = follower.Bio
                });
            }
        }

        return followers;
    }
}