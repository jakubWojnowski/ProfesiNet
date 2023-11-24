﻿using MediatR;
using ProfesiNet.Users.Application.Educations.Dtos;

namespace ProfesiNet.Users.Application.Educations.Queries.Get;

public record GetUserEducationByIdQuery(Guid EducationId, Guid UserId) : IRequest<GetEducationDto>;