using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;

namespace ProfesiNet.Users.Application.Users.Queries.Get;

internal record GetOwnProfileQuery(Guid UserId) : IRequest<UserDto>;