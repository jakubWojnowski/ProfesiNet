using MediatR;

namespace ProfesiNet.Users.Application.Educations.Commands.Update;

public record UpdateUserEducationCommand( Guid Id,string? Name, string? Description, string? Degree,string? FieldOfStudy, DateTime? StartDate, DateTime? EndDate) : IRequest;
