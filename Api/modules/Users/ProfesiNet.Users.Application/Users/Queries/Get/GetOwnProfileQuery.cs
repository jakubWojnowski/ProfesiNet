using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;

namespace ProfesiNet.Users.Application.Users.Queries.Get;

public record GetOwnProfileQuery() : IRequest<UserDto>;