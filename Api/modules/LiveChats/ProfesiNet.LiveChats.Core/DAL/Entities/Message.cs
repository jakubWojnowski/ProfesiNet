﻿namespace ProfesiNet.LiveChats.Core.DAL.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public Guid ChatMemberId { get; set; }
    public DateTime Date { get; set; }
    // public Guid? SenderId { get; set; }
    // public Guid?  ReceiverId { get; set; }
    
    public Guid? ChatId { get; set; }
    public virtual Chat? Chat { get; set; }
}