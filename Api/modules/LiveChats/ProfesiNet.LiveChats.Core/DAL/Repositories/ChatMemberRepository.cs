using ProfesiNet.LiveChats.Core.DAL.Entities;
using ProfesiNet.LiveChats.Core.DAL.Persistence;

namespace ProfesiNet.LiveChats.Core.DAL.Repositories;

internal class ChatMemberRepository : GenericRepository<ChatMember,Guid>, IChatMemberRepository
{
    public ChatMemberRepository(ProfesiNetLiveChatsDbContext dbContext) : base(dbContext)
    {
    }
}