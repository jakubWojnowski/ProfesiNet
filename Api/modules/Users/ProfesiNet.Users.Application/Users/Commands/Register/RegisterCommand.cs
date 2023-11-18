using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Responses;

namespace ProfesiNet.Users.Application.Users.Commands.Register;

public record RegisterCommand(RegisterDto RegisterDto) : IRequest;

