using Application.UseCases.Permissions.Commands;
using Application.UseCases.Permissions.Queries;
using CommentPost.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PermissionController : ApiBaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> CreatePermission([FromForm] CreatePermissionCommand command) => Ok(await _mediatr.Send(command));

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeletePermission([FromForm] DeletePermissionCommand command) => Ok(await _mediatr.Send(command));

    [HttpPut("Update")]
    public async Task<IActionResult> UpdatePermission([FromForm] UpdatePermissionCommand command) => Ok(await _mediatr.Send(command));

    [LazyCachePro(50, 100)]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllPermissions() => Ok(await _mediatr.Send(new GetAllPermissionQuery()));

    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdPermission([FromForm] GetByIdPermissionQuery query) => Ok(await _mediatr.Send(query));
}

