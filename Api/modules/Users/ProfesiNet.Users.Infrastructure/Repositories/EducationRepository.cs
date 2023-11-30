using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Persistence;

namespace ProfesiNet.Users.Infrastructure.Repositories;

public class EducationRepository : GenericRepository<Education, Guid>, IEducationRepository
{
    public EducationRepository(ProfesiNetUserDbContext dbContext) : base(dbContext)
    {
    }
}
//to do Dev mentorsi stwierdzili ze generyczne repo jest okay ale w przypadku crudow i moge je uzyc jako bazowe dla innych repo ale w kompozycji najlepiej.
//przemyslec czy modul users moze miec generyczne repo i czy to nie bedzie problemem w przyszlosci
//napewno modul Post bedzie mial genereyczne repo bo tam beda tylko crudy i nie bedzie problemu z kompozycja