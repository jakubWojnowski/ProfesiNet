using MediatR;
using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Application.Educations.Mappings;
using ProfesiNet.Users.Application.UserContext;
using ProfesiNet.Users.Domain.Exceptions;
using ProfesiNet.Users.Domain.Interfaces;

namespace ProfesiNet.Users.Application.Educations.Commands.Update;

public class UpdateUserEducationCommandHandler : IRequestHandler<UpdateUserEducationCommand>
{
    private readonly IEducationRepository _educationRepository;
    private readonly ICurrentUserContextService _currentUserContextService;
    private static readonly EducationMapper Mapper = new();

    public UpdateUserEducationCommandHandler(IEducationRepository educationRepository, ICurrentUserContextService currentUserContextService)
    {
        _educationRepository = educationRepository;
        _currentUserContextService = currentUserContextService;
    }
    public async Task Handle(UpdateUserEducationCommand request, CancellationToken cancellationToken)
    {
        var tokenId = Guid.TryParse(_currentUserContextService.GetCurrentUser()?.Id, out var id) ? id : Guid.Empty;
        var education = await _educationRepository.GetRecordByFilterAsync(e=> e.Id == request.Id && e.UserId == tokenId, cancellationToken);
        if (education == null)
        {
            throw new NotFoundException("Education not found");
        } 
        var educationToUpdateDto = new EducationDto()
        {
            Name = request.Name ?? education.Name,
            Description = request.Description ?? education.Description,
            Degree = request.Degree ?? education.Degree,
            FieldOfStudy = request.FieldOfStudy ?? education.FieldOfStudy,
            StartDate = request.StartDate ?? education.StartDate,
            EndDate = request.EndDate ?? education.EndDate
        };
        var updatedEducation = Mapper.MapUpdateEducationDtoToEducation(education, educationToUpdateDto);
        await _educationRepository.UpdateAsync(updatedEducation, cancellationToken);
    }
}