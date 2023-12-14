using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal record DeleteConnectionInvitationSentCommand(Guid UserId, Guid TargetId) : IRequest;