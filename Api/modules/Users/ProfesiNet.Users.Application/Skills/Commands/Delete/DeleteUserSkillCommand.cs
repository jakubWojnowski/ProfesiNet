using MediatR;

namespace ProfesiNet.Users.Application.Skills.Commands.Delete;

internal record DeleteUserSkillCommand(Guid Id, Guid UserId) : IRequest;
