using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Users.Application.Educations.Mappings;
[Mapper]
public partial class EducationMapper
{
    public partial Education MapAddEducationDtoToEducation(AddEducationDto addEducationDto);
    public partial EducationDto EducationToEducationDto(Education education);
    public partial IReadOnlyList<Education> EducationDtosToEducations(IReadOnlyList<EducationDto> educationDtos);
    
   
    public partial Education AddEducationDtoToEducation(AddEducationDto addEducationDto);
    
}