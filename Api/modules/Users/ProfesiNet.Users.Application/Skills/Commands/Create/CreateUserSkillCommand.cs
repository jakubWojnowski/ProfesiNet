using MediatR;
using ProfesiNet.Shared.Commands;

namespace ProfesiNet.Users.Application.Skills.Commands.Create;

internal record CreateUserSkillCommand(List<string> Names, Guid UserId) : ICommand;

