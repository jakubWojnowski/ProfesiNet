using MediatR;

namespace ProfesiNet.Users.Application.Users.Commands.Update;

public record UpdateUserBioCommand(string Bio) : IRequest;
