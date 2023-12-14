using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Queries.GetAll;

internal class GetAllUserReceivedInvitationsSentQueryHandler : IRequestHandler<GetAllUserReceivedInvitationsSentQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUserReceivedInvitationsSentQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<UserDto>> Handle(GetAllUserReceivedInvitationsSentQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var invitationGuids = user.NetworkConnectionInvitationsSent;
        var invitationSenders = new List<UserDto>();

        foreach (var id in invitationGuids)
        {
            var sender = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (sender != null)
            {
                invitationSenders.Add(new UserDto
                {
                    Id = sender.Id,
                    Name = sender.Name,
                    Surname = sender.Surname,
                    Address = sender.Address,
                    Bio = sender.Bio
                });
            }
        }

        return invitationSenders;
    }
}