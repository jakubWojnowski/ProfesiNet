using MediatR;

namespace ProfesiNet.Users.Application.Networks.Conncections.Commands.Create.SendConnectionRequest;

internal record SendConnectionRequestCommand(Guid SenderId, Guid TargetId) : IRequest<Guid>;