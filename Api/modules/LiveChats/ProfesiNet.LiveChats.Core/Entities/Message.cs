namespace ProfesiNet.LiveChats.Core.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public DateTime Date { get; set; }
    public Guid? SenderId { get; set; }
    public Guid?  ReceiverId { get; set; }
    
    public Guid? ChatId { get; set; }
    public virtual Chat? Chat { get; set; }
}