using Application.UseCases.Posts.Commands;
using Application.UseCases.Posts.Query;
using CommentPost.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PostController : ApiBaseController
{
    [HttpPost("Create")]
    public async Task<IActionResult> CreatePost([FromForm] CreatePostCommand command) => Ok(await _mediatr.Send(command));

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeletePost([FromForm] DeletePostCommand command) => Ok(await _mediatr.Send(command));

    [HttpPut("Update")]
    public async Task<IActionResult> UpdatePost([FromForm] UpdatePostCommand command) => Ok(await _mediatr.Send(command));

    [LazyCachePro(10, 20)]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllPosts() => Ok(await _mediatr.Send(new GetAllPostQuery()));

    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdPost([FromForm] GetByIdPostQuery query) => Ok(await _mediatr.Send(query));
}

