using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal record UpdateUserAddressCommand(string? Address) : IRequest;
