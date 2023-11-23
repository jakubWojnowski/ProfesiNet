using MediatR;
using ProfesiNet.Users.Application.Experiences.Dtos;

namespace ProfesiNet.Users.Application.Experiences.Queries.Get;

public record GetUserExperienceByIdQuery(Guid Id) : IRequest<GetExperienceDto>;