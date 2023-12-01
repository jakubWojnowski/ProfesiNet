using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;

namespace ProfesiNet.Users.Application.Users.Queries.Get;

internal record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
