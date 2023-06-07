using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommentPost.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiBaseController : Controller
{
    protected IMediator _mediatr => HttpContext.RequestServices.GetRequiredService<IMediator>();
}