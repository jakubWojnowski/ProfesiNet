using MediatR;
using ProfesiNet.Users.Application.Skills.Dtos;

namespace ProfesiNet.Users.Application.Skills.Queries.Get;

internal record GetSkillByIdQuery(Guid Id) : IRequest<SkillDto>;