using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;

namespace ProfesiNet.Users.Application.Users.Queries.GetAll;

internal record GetAllUserConnectionsQuery(Guid UserId) : IRequest<IEnumerable<UserDto>>;