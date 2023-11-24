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