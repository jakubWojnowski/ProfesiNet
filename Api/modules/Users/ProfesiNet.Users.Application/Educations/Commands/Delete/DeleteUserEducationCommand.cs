using MediatR;

namespace ProfesiNet.Users.Application.Educations.Commands.Delete;

public record DeleteUserEducationCommand(Guid Id): IRequest;
