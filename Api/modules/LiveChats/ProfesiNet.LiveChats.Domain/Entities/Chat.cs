namespace ProfesiNet.LiveChats.Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid? FirstUserId { get; set; }
    public Guid? SecondUserId { get; set; }
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
