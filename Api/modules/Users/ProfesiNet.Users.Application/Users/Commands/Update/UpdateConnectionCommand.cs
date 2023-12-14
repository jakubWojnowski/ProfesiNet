using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal record UpdateConnectionCommand(Guid UserId, Guid TargetId) : IRequest;