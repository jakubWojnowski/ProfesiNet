using MediatR;

namespace ProfesiNet.Users.Application.Educations.Commands.Create;

public record CreateUserEducationCommand(string Name, string Description, string? Degree,string? FieldOfStudy, DateOnly StartDate, DateOnly? EndDate ) : IRequest<Guid>;