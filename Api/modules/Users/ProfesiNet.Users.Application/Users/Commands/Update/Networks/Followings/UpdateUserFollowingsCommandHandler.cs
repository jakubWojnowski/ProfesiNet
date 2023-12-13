using System.Transactions;
using MediatR;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Update.Networks.Followings;

internal class UpdateUserFollowingsCommandHandler : IRequestHandler<UpdateUserFollowingsCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserFollowingsCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UpdateUserFollowingsCommand request, CancellationToken cancellationToken) =>
        await _userRepository.UpdateFollowingsAsync(request.UserId, request.TargetId, cancellationToken);
}