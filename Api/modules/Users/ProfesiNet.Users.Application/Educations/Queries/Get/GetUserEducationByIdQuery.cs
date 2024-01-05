using MediatR;
using ProfesiNet.Users.Application.Educations.Dtos;

namespace ProfesiNet.Users.Application.Educations.Queries.Get;

internal record GetUserEducationByIdQuery(Guid EducationId) : IRequest<EducationDto>;