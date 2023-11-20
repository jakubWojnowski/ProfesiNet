using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

public record DeleteUserCommand(Guid Id) : IRequest;
