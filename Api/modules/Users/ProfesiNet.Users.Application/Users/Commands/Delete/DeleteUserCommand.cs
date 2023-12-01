using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal record DeleteUserCommand(Guid Id) : IRequest;
