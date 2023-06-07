using Application.UseCases.Roles.Command;
using Application.UseCases.Roles.Query;
using CommentPost.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ApiBaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> CreateRole([FromForm] CreateRoleCommand command) => Ok(await _mediatr.Send(command));

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteRole([FromForm] DeleteRoleCommand command) => Ok(await _mediatr.Send(command));

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateRole([FromForm] UpdateRoleCommand command) => Ok(await _mediatr.Send(command));

   // [LazyCachePro(10, 20)]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllRoles() => Ok(await _mediatr.Send(new GetAllRoleQuery()));

    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdRole([FromForm] GetByIdRoleQuery query) => Ok(await _mediatr.Send(query));
}
