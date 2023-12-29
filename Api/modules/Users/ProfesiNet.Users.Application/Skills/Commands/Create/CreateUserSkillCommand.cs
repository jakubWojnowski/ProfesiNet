using MediatR;

namespace ProfesiNet.Users.Application.Skills.Commands.Create;

internal record CreateUserSkillCommand(List<string> Names, Guid UserId) : IRequest;

