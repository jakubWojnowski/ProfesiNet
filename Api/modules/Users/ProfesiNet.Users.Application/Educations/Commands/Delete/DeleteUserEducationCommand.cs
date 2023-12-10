using MediatR;

namespace ProfesiNet.Users.Application.Educations.Commands.Delete;

internal record DeleteUserEducationCommand(Guid Id, Guid UserId): IRequest;
