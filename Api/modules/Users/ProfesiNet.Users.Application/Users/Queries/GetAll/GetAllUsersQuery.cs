using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;

namespace ProfesiNet.Users.Application.Users.Queries.GetAll;

internal record GetAllUsersQuery() : IRequest<IReadOnlyCollection<UserDto>>;
