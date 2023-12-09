using MediatR;

namespace ProfesiNet.Users.Application.Skills.Commands.Create;

internal record CreateUserSkillCommand(string Name, Guid UserId) : IRequest<Guid>;

