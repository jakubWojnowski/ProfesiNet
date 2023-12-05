using MediatR;

namespace ProfesiNet.Users.Application.Skills.Commands.Create;

internal record CreateSkillCommand(string Name) : IRequest<Guid>
{
    public Guid Id { get; init; }

}

