using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Persistence;

namespace ProfesiNet.Users.Infrastructure.Repositories;

internal class SkillRepository : GenericRepository<Skill,Guid>, ISkillRepository
{
    public SkillRepository(ProfesiNetUserDbContext dbContext) : base(dbContext)
    {
    }
}