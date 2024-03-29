﻿using MediatR;
using ProfesiNet.Shared.Messaging;
using ProfesiNet.Users.Application.Events;
using ProfesiNet.Users.Application.Users.Mappings;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal class UpdateUserFullNameCommandHandler : IRequestHandler<UpdateUserFullNameCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageBroker _messageBroker;
    private static readonly UserMapper Mapper = new();

    public UpdateUserFullNameCommandHandler(IUserRepository userRepository, IMessageBroker messageBroker)
    {
        _userRepository = userRepository;
        _messageBroker = messageBroker;
    }

    public async Task Handle(UpdateUserFullNameCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }
  
        user.Name = request.Name ?? user.Name;
        user.Surname = request.Surname ?? user.Surname;

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _messageBroker.PublishMessageAsync(new UserFullNameUpdated(user.Id, user.Name, user.Surname));
    }
}