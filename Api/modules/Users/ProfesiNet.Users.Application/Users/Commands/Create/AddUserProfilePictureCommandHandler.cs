using MediatR;
using ProfesiNet.Shared.Photos;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Users.Commands.Create;

internal class AddUserProfilePictureCommandHandler : IRequestHandler<AddUserProfilePictureCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPhotoAccessor _photoAccessor;

    public AddUserProfilePictureCommandHandler(IUserRepository userRepository, IPhotoAccessor photoAccessor)
    {
        _userRepository = userRepository;
        _photoAccessor = photoAccessor;
    }

    public async Task<string> Handle(AddUserProfilePictureCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var photoUploadResult = await _photoAccessor.AddPhoto(request.File);
        if (photoUploadResult == null)
        {
            throw new Exception("Problem adding photo");
        }

        user.ProfilePictureUrl = photoUploadResult.Url;
        user.ProfilePicturePublicId = photoUploadResult.PublicId;

        await _userRepository.UpdateAsync(user, cancellationToken);


        return user.ProfilePicturePublicId;
    }
}