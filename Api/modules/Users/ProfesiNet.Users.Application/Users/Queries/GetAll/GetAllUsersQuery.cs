using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;

namespace ProfesiNet.Users.Application.Users.Queries.GetAll;

public record GetAllUsersQuery() : IRequest<IReadOnlyList<UserDto>>;
