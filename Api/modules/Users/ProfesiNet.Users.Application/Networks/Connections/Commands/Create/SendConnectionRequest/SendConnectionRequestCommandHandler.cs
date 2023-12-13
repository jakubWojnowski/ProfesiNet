using MediatR;
using ProfesiNet.Users.Application.Networks.Conncections.Commands.Create.SendConnectionRequest;
using ProfesiNet.Users.Application.Networks.Connections.Mappings;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Networks.Connections.Commands.Create.SendConnectionRequest;

internal class SendConnectionRequestCommandHandler : IRequestHandler<SendConnectionRequestCommand, Guid>
{
    private readonly INetworkConnectionRepository _networkConnectionRepository;
    private static readonly ConnectionMapper Mapper = new();

    public SendConnectionRequestCommandHandler(INetworkConnectionRepository networkConnectionRepository)
    {
        _networkConnectionRepository = networkConnectionRepository;
    }
    public async Task<Guid> Handle(SendConnectionRequestCommand request, CancellationToken cancellationToken)
    {
        var connection = Mapper.MapSendConnectionRequestCommandToConnection(request);

        var existingRequest = await _networkConnectionRepository.GetForConditionAsync(
            x => (x.SenderId == request.SenderId && x.TargetId == request.TargetId) || 
                 (x.SenderId == request.TargetId && x.TargetId == request.SenderId),
            cancellationToken);

        if (existingRequest != null)
        {
            throw new InvalidOperationException("Connection request already exists.");
        }

        await _networkConnectionRepository.AddAsync(connection, cancellationToken);
        return connection.Id;
    }
    
}