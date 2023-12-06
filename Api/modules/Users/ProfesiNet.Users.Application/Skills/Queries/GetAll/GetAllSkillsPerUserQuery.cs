using MediatR;
using ProfesiNet.Users.Application.Skills.Dtos;

namespace ProfesiNet.Users.Application.Skills.Queries.GetAll;

internal record GetAllSkillsPerUserQuery(Guid Id) : IRequest<IReadOnlyCollection<SkillDto>>;  