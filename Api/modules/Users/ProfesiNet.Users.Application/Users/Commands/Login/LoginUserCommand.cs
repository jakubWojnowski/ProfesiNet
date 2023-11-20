using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;

namespace ProfesiNet.Users.Application.Users.Commands.Login;

public record LoginUserCommand(string Email, string Password) : IRequest<string>;