using ProfesiNet.Users.Domain.Enums;

namespace ProfesiNet.Users.Domain.Entities;

public class NetworkConnection
{
    public Guid Id { get; set; } 
    public Guid? TargetId { get; set; } 
    public Guid? SenderId { get; set; } 
    public RelationshipType Type { get; set; } 
    public DateTime? RequestDate { get; set; } 
    public bool IsApproved { get; set; } 
    
    public virtual User? Target { get; set; } 
    public virtual User? Sender { get; set; } 
    
}