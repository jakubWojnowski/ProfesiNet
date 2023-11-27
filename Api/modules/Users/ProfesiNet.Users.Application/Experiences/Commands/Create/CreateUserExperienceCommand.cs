﻿using MediatR;
using ProfesiNet.Users.Application.Experiences.Dtos;

namespace ProfesiNet.Users.Application.Experiences.Commands.Create;

public record CreateUserExperienceCommand(string Company, string Position, string Description, DateTime StartDate,  DateTime? EndDate ) : IRequest<Guid>;