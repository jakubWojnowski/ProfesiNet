using MediatR;
using Microsoft.AspNetCore.Http;

namespace ProfesiNet.Users.Application.Users.Commands.Create;

internal record AddUserProfilePictureCommand(Guid UserId,IFormFile File) : IRequest<string>;

 