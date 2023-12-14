using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal record UpdateConnectionInvitationCommand(Guid UserId, Guid TargetId) : IRequest;