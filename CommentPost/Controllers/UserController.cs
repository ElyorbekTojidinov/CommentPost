using Application.UseCases.Users.Command;
using Application.UseCases.Users.Queries;
using CommentPost.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ApiBaseController
{
    
    [HttpPost("Create")]
    public async Task<IActionResult> CreateUser([FromForm] CreateUserCommand command) => Ok(await _mediatr.Send(command));

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteUser([FromForm] DeleteUserCommand command) => Ok(await _mediatr.Send(command));

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserCommand command) => Ok(await _mediatr.Send(command));

    [LazyCachePro(10, 20)]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAlUserts() => Ok(await _mediatr.Send(new GetAllUserQuery()));

    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdUser([FromForm] GetByIdUserQuery query) => Ok(await _mediatr.Send(query));
}

