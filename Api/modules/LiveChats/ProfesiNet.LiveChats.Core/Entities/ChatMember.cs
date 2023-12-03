namespace ProfesiNet.LiveChats.Core.Entities;

public class ChatMember
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
  
    
}