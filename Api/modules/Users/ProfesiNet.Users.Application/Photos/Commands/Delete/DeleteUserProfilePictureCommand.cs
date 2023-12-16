using MediatR;

namespace ProfesiNet.Users.Application.Photos.Commands.Delete;

internal record DeleteUserProfilePictureCommand(Guid UserId, Guid PhotoId) : IRequest;