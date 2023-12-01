using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;
using ProfesiNet.Users.Application.Users.Responses;

namespace ProfesiNet.Users.Application.Users.Commands.Register;

internal record RegisterUserCommand(string Email, string Name, string Surname, string Password, string ConfirmPassword) : IRequest;

