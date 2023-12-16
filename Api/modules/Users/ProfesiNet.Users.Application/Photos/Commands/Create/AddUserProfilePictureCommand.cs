using MediatR;
using Microsoft.AspNetCore.Http;
using ProfesiNet.Users.Application.Photos.Dtos;
using ProfesiNet.Users.Domain.Entities;

namespace ProfesiNet.Users.Application.Photos.Commands.Create;

internal record AddUserProfilePictureCommand(Guid UserId,IFormFile File) : IRequest<string>;

 