using MediatR;

namespace ProfesiNet.Users.Application.Experiences.Commands.Update;

internal record UpdateUserExperienceCommand(Guid Id, string? Company, string? Position, string? Description, DateTime? StartDate, DateTime? EndDate, Guid UserId) : IRequest;
