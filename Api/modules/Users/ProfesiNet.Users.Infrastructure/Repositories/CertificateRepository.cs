using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Persistence;

namespace ProfesiNet.Users.Infrastructure.Repositories;

public class CertificateRepository : GenericRepository<Certificate,Guid>, ICertificateRepository
{
    public CertificateRepository(ProfesiNetUserDbContext dbContext) : base(dbContext)
    {
    }
}