using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Delete;

internal record DeleteOwnAccountCommand(Guid Id) : IRequest;