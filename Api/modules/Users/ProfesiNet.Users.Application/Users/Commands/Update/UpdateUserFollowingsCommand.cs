using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal record UpdateUserFollowingsCommand(Guid UserId, Guid TargetId ) : IRequest;