using ProfesiNet.Users.Application.Experiences.Dtos;
using ProfesiNet.Users.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProfesiNet.Users.Application.Experiences.Mappings;
[Mapper]
public partial class ExperienceMapper
{
    public partial Experience MapAddExperienceDtoToExperience(ExperienceDto experienceDto);

    public Experience MapUpdateExperienceDtoToExperience(Experience experience, ExperienceDto experienceDto)
    {
        experience.Company = experienceDto.Company;
        experience.Position = experienceDto.Position;
        experience.Description = experienceDto.Description;
        experience.StartDate = experienceDto.StartDate;
        experience.EndDate = experienceDto.EndDate;
        return experience;
    }
    

}