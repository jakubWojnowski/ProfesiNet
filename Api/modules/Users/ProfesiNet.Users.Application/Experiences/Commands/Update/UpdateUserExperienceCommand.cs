using MediatR;

namespace ProfesiNet.Users.Application.Experiences.Commands.Update;

public record UpdateUserExperienceCommand(Guid Id, string? Company, string? Position, string? Description, DateOnly? StartDate, DateOnly? EndDate) : IRequest;
