using Domain.Common;
using Domain.Entities;

namespace Domain.IdentityEntity;

public class Role : BaseAuditibleEntity
{
    public string RoleName { get; set; }
    public virtual List<Permission>? Permissions { get; set; }
    public virtual List<User>? Users { get; set; }
}
