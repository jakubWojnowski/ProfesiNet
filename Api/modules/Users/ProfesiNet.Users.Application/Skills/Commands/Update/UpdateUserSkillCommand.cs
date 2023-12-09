using MediatR;

namespace ProfesiNet.Users.Application.Skills.Commands.Update;

internal record UpdateUserSkillCommand(string Name, Guid Id, Guid UserId) : IRequest;
