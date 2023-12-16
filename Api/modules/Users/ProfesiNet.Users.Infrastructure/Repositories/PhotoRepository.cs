using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Persistence;

namespace ProfesiNet.Users.Infrastructure.Repositories;

internal class PhotoRepository : GenericRepository<Photo, Guid>, IPhotoRepository
{
    public PhotoRepository(ProfesiNetUserDbContext dbContext) : base(dbContext)
    {
    }
}