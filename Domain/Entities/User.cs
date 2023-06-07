using Domain.Common;
using Domain.IdentityEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("users")]
public class User : BaseAuditibleEntity
{
    [Column("username")]
    public string Username { get; set; }
    [Column("password")]
    public string Password { get; set; }
    public virtual List<Post>? Posts { get; set; }
    public virtual List<Comment>? Comments { get; set; }
    public virtual List<Role>? Roles { get; set; }
}
