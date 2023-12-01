using MediatR;
using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Application.Users.Dtos;

namespace ProfesiNet.Users.Application.Users.Queries.Get;

internal record GetUserAndExperienceQuery(Guid Id) : IRequest<UserAndExperienceDto>;
