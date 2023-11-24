using MediatR;
using ProfesiNet.Users.Application.Experiences.Dtos;

namespace ProfesiNet.Users.Application.Experiences.Queries.GetAll;

public record GetAllUserExperienceQuery(Guid Id) : IRequest<IEnumerable<GetExperienceDto>>;