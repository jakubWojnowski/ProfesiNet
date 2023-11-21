using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

public record UpdateUserAddressCommand(string Address) : IRequest;
