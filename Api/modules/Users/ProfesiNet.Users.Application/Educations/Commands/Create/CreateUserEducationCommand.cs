using MediatR;

namespace ProfesiNet.Users.Application.Educations.Commands.Create;

internal record CreateUserEducationCommand(string Name, string Address, string? Degree,string? FieldOfStudy, DateTime StartDate, DateTime? EndDate, Guid UserId ) : IRequest<Guid>;