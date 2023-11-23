using MediatR;
using ProfesiNet.Users.Application.Experiences.Dtos;

namespace ProfesiNet.Users.Application.Experiences.Queries.GetAll;

public record GetAllUserExperienceQuery() : IRequest<IEnumerable<GetExperienceDto>>;