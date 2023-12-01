using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Logout;

internal record LogoutUserCommand() : IRequest<bool>;
