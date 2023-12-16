namespace ProfesiNet.Posts.Core.DAL.Entities;

public class Creator
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? ProfilePicture { get; set; }
    public IList<Guid> Followings { get; set; } = new List<Guid>();
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}