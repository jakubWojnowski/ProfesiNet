﻿using MediatR;
using ProfesiNet.Shared.Messaging;
using ProfesiNet.Shared.Photos;
using ProfesiNet.Users.Application.Events;
using ProfesiNet.Users.Domain.Enums;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Photos.Commands.Delete;

internal class DeleteUserProfilePictureCommandHandler : IRequestHandler<DeleteUserProfilePictureCommand>
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IPhotoAccessor _photoAccessor;
    private readonly IMessageBroker _messageBroker;

    public DeleteUserProfilePictureCommandHandler( IPhotoRepository photoRepository, IPhotoAccessor photoAccessor, IMessageBroker messageBroker)
    {
        _photoRepository = photoRepository;
        _photoAccessor = photoAccessor;
        _messageBroker = messageBroker;
    }
    public async Task Handle(DeleteUserProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var photo = await _photoRepository.GetRecordByFilterAsync(p => p.Id == request.PhotoId && p.UserId == request.UserId && p.PictureType == PictureType.ProfilePicture, cancellationToken);
        if (photo is null)
        {
            throw new ProfilePictureNotFoundException(request.PhotoId, request.UserId);
        }
        
        if (photo.PublicId is not null)
        {
            var result = await _photoAccessor.DeletePhoto(photo.PublicId);
            if (result is null)
            {
                throw new Exception("Problem deleting photo");
            }
        }

        await _photoRepository.DeleteAsync(photo, cancellationToken);
        await _messageBroker.PublishMessageAsync(new UserProfilePictureDeleted(request.UserId));
    }
}