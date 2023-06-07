using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;


[Table("posts")]
public class Post : BaseAuditibleEntity
{
    [Column("title")]
    public string Title { get; set; }
    [Column("content")]
    public string Content { get; set; }
    [Column("author")]
    public Guid AuthorId { get; set; }
    public virtual User Author { get; set; }
    public virtual List<Comment>? Comments { get; set; }
}
