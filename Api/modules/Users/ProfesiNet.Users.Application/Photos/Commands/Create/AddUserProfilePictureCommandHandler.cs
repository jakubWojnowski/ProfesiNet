using MediatR;
using ProfesiNet.Shared.Photos;
using ProfesiNet.Users.Application.Photos.Dtos;
using ProfesiNet.Users.Application.Photos.Mappings;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Enums;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Photos.Commands.Create;

internal class AddUserProfilePictureCommandHandler : IRequestHandler<AddUserProfilePictureCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IPhotoAccessor _photoAccessor;
    private static readonly PhotoMapper Mapper = new();

    public AddUserProfilePictureCommandHandler(IUserRepository userRepository, IPhotoRepository photoRepository,
        IPhotoAccessor photoAccessor)
    {
        _userRepository = userRepository;
        _photoRepository = photoRepository;
        _photoAccessor = photoAccessor;
    }

    public async Task<string> Handle(AddUserProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }


        var photoUploadResult = await _photoAccessor.AddPhoto(request.File);
        if (photoUploadResult == null)
        {
            throw new Exception("Problem adding photo");
        }

        var photoDto = new PhotoDto
        {
            Url = photoUploadResult.Url,
            PublicId = photoUploadResult.PublicId,
            PictureType = PictureType.ProfilePicture,
            UserId = user.Id
        };
        var photo = Mapper.MapPhotoDtoToPhoto(photoDto);
        await _photoRepository.AddAsync(photo, cancellationToken);


        return photoDto.Url;
    }
}