using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;

namespace ProfesiNet.Users.Application.Users.Commands.Login;

public record LoginCommand(LoginDto LoginDto) : IRequest<string>;