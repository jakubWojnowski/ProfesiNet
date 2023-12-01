using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

internal record UpdateUserBioCommand(string? Bio) : IRequest;
