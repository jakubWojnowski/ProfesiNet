using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal record UpdateUserFullNameCommand(string? Name, string? Surname, Guid UserId) : IRequest;
