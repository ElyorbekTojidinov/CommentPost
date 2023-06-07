using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("comment")]
public class Comment : BaseAuditibleEntity
{
    [Column("content")]
    public string Content { get; set; }
    [Column("author")]
    public Guid AuthorId { get; set; }
    public virtual User Author { get; set; }
    [Column("post")]
    public Guid PostId { get; set; }
    public virtual Post Post { get; set; }
    public virtual List<Comment>? ChildComments { get; set; }
}

