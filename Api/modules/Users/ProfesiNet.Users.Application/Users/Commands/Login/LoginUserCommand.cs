using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;

namespace ProfesiNet.Users.Application.Users.Commands.Login;

internal record LoginUserCommand(string Email, string Password) : IRequest<string>;