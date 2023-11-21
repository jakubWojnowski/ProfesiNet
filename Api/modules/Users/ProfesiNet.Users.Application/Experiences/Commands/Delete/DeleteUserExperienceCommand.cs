using MediatR;

namespace ProfesiNet.Users.Application.Experiences.Commands.Delete;

public record DeleteUserExperienceCommand(Guid Id) : IRequest;
