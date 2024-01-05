using ProfesiNet.Users.Application.Educations.Dtos;
using ProfesiNet.Users.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Users.Application.Educations.Mappings;
[Mapper]
internal partial class EducationMapper
{
    public partial Education MapEducationDtoToEducation(EducationDto educationDto);
    public partial EducationDto MapEducationToEducationDto(Education education);
    public partial IReadOnlyCollection<EducationDto> MapEducationDtosToEducations(IEnumerable<Education?> educations);
    
    public  Education MapUpdateEducationDtoToEducation(Education education, EducationDto educationDto)
    {
        education.Name = educationDto.Name;
        education.Address = educationDto.Address;
        education.FieldOfStudy = educationDto.FieldOfStudy;
        education.StartDate = educationDto.StartDate;
        education.EndDate = educationDto.EndDate;
        return education;
        
    }
    public partial GetEducationDto EducationToGetEducationDto(Education education);
    public partial IReadOnlyCollection<GetEducationDto> GetEducationDtosToEducations(IEnumerable<Education?> educations);
    
   
    public partial Education AddEducationDtoToEducation(EducationDto educationDto);
    
}