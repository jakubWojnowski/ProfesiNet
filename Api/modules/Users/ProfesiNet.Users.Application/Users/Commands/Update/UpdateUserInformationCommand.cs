using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

public record UpdateUserInformationCommand(Guid UserId, string? Name, string? Surname, string? Title, string? Address ) : IRequest<UserDetailsDto>;