using MediatR;

namespace ProfesiNet.Users.Application.Educations.Commands.Create;

internal record CreateUserEducationCommand(string Name, string Description, string? Degree,string? FieldOfStudy, DateTime StartDate, DateTime? EndDate ) : IRequest<Guid>;