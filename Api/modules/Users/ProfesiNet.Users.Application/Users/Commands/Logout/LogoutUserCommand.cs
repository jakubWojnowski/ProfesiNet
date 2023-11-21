using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Logout;

public record LogoutUserCommand() : IRequest<bool>;
