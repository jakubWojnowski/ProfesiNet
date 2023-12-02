﻿using ProfesiNet.Posts.Core.Entities;
using ProfesiNet.Posts.Core.Interfaces;
using ProfesiNet.Posts.Core.Persistence;

namespace ProfesiNet.Posts.Core.Repositories;

internal class PostLikeRepository : GenericRepository<PostLike,Guid>, IPostLikeRepository
{
    public PostLikeRepository(ProfesiNetPostDbContext dbContext) : base(dbContext)
    {
    }
}