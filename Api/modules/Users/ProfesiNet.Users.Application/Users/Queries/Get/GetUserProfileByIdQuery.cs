using MediatR;
using ProfesiNet.Users.Application.Users.Dtos;

namespace ProfesiNet.Users.Application.Users.Queries.Get;

internal record GetUserProfileByIdQuery(Guid Id) : IRequest<ProfileDto>;