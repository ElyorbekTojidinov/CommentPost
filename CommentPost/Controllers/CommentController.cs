using Application.UseCases.Comments.Commands;
using Application.UseCases.Comments.Queries;
using CommentPost.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ApiBaseController
{
   
    [HttpPost("Create")]
    public async Task<IActionResult> CreateComment([FromForm] CreateCommentCommand command)
    {
        return Ok(await _mediatr.Send(command));
    }


    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteComment([FromForm] DeleteCommentCommand command)
    {
        return Ok(await _mediatr.Send(command));
    }


    [HttpPut("Update")]
    public async Task<IActionResult> UpdateComment([FromForm] UpdateCommentCommand command)
    {
        return Ok(await _mediatr.Send(command));
    }

    
    [LazyCachePro(10, 20)]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllComments()
    {
        return Ok(await _mediatr.Send(new GetAllCommentQuery()));
    }


    [HttpGet("GetById")]
    public async Task<IActionResult> GetByIdComment([FromForm] GetByIdCommentQuery query)
    {
        return Ok(await _mediatr.Send(query));
    }
}
