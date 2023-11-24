using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Users.Application.Educations.Mappings;
[Mapper]
public partial class EducationMapper
{
    public partial Education MapEducationDtoToEducation(EducationDto educationDto);
    
    public  Education MapUpdateEducationDtoToEducation(Education education, EducationDto educationDto)
    {
        education.Name = educationDto.Name;
        education.Description = educationDto.Description;
        education.FieldOfStudy = educationDto.FieldOfStudy;
        education.StartDate = educationDto.StartDate;
        education.EndDate = educationDto.EndDate;
        return education;
        
    }
    public partial GetEducationDto EducationToGetEducationDto(Education education);
    public partial IReadOnlyCollection<GetEducationDto> GetEducationDtosToEducations(IEnumerable<Education?> educations);
    
   
    public partial Education AddEducationDtoToEducation(EducationDto educationDto);
    
}