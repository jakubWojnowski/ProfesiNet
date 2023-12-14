using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Queries.GetAll;

internal class GetAllUserConnectionsQueryHandler : IRequestHandler<GetAllUserConnectionsQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUserConnectionsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<UserDto>> Handle(GetAllUserConnectionsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var connectionsGuids = user.NetworkConnections;
        var connections = new List<UserDto>();

        foreach (var id in connectionsGuids)
        {
            var connection = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (connection != null)
            {
                connections.Add(new UserDto
                {
                    Id = connection.Id,
                    Name = connection.Name,
                    Surname = connection.Surname,
                    Address = connection.Address,
                    Bio = connection.Bio
                });
            }
        }

        return connections;
    }
}