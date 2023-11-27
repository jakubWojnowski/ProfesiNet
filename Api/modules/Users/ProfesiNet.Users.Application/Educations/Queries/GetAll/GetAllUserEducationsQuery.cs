﻿using MediatR;
using ProfesiNet.Users.Application.Educations.Dtos;

namespace ProfesiNet.Users.Application.Educations.Queries.GetAll;

public record GetAllUserEducationsQuery(Guid Id) : IRequest<IReadOnlyCollection<GetEducationDto>>;