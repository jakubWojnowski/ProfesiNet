using MediatR;
using ProfesiNet.Users.Application.Educations.Dtos;

namespace ProfesiNet.Users.Application.Educations.Queries.Get;

internal record GetEducationByIdQuery(Guid EducationId) : IRequest<GetEducationDto>;