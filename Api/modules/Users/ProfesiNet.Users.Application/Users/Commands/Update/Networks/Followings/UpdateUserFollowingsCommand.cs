using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Update.Networks.Followings;

public record UpdateUserFollowingsCommand(Guid UserId, Guid TargetId ) : IRequest;