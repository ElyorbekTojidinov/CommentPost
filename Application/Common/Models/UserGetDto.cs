namespace Application.Common.Models;

public class UserGetDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public List<RoleGetDto>? Roles { get; set; }
}
