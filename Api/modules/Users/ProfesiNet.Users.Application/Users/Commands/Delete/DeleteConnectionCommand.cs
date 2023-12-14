using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal record DeleteConnectionCommand(Guid UserId, Guid TargetId) : IRequest;