using Domain.Common;

namespace Domain.IdentityEntity;

public class Permission : BaseAuditibleEntity
{
    public string PermissionName { get; set; }
    public virtual List<Role>? Roles { get; set; }
}

