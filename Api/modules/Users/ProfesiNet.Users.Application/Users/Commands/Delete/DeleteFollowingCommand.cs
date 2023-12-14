using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal record DeleteFollowingCommand(Guid UserId, Guid TargetId) : IRequest;