using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

public record DeleteConnectionInvitationReceivedCommand(Guid UserId, Guid TargetId) : IRequest;