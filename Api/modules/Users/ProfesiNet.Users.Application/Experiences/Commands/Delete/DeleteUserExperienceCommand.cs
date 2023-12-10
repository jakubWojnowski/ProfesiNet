using MediatR;

namespace ProfesiNet.Users.Application.Experiences.Commands.Delete;

internal record DeleteUserExperienceCommand(Guid Id, Guid UserId) : IRequest;
