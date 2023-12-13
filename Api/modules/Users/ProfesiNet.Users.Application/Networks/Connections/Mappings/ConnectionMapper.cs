using Microsoft.EntityFrameworkCore;
using ProfesiNet.Users.Application.Networks.Conncections.Commands.Create.SendConnectionRequest;
using ProfesiNet.Users.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Users.Application.Networks.Connections.Mappings;
[Mapper]
internal partial class ConnectionMapper
{
    
    public partial NetworkConnection MapSendConnectionRequestCommandToConnection(SendConnectionRequestCommand command);
}