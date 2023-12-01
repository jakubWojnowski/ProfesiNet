using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Queries.GetAll;

internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IReadOnlyCollection<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private static readonly UserMapper Mapper = new();

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IReadOnlyCollection<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        return Mapper.UserDtosToUsers(users);
    }
}