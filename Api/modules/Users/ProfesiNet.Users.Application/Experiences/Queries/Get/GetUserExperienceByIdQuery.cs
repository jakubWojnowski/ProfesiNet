using MediatR;
using ProfesiNet.Users.Application.Experiences.Dtos;

namespace ProfesiNet.Users.Application.Experiences.Queries.Get;

internal record GetUserExperienceByIdQuery(Guid ExperienceId, Guid UserId) : IRequest<GetExperienceDto>;