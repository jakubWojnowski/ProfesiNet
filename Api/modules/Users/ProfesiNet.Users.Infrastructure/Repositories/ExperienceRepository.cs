using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Persistence;

namespace ProfesiNet.Users.Infrastructure.Repositories;

public class ExperienceRepository : GenericRepository<Experience, Guid>, IExperienceRepository
{
    public ExperienceRepository(ProfesiNetUserDbContext dbContext) : base(dbContext)
    {
    }
}