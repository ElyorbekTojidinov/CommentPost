using Application.Common.Exceptions;
using Application.Common.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CommentPost.Middelwares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IMediator _mediator;
    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IMediator mediator)
    {
        _logger = logger;
        _next = next;
        _mediator = mediator;
    }


    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException ex)
        {
            await HandleException(httpContext, ex.Message, HttpStatusCode.NotFound, ex.Message);
            await _mediator.Publish(new ExceptionNotification()
            {
                 ExceptionString = ex.Message
            });
        }
        catch (Exception ex)
        {
            await HandleException(httpContext, ex.Message, HttpStatusCode.InternalServerError, ex.Message);
            await _mediator.Publish(new ExceptionNotification()
            {
                ExceptionString = ex.Message
            });
        }

    }


    public async ValueTask<ActionResult> HandleException(HttpContext httpContext, string exMessage, HttpStatusCode httpStatusCode, string message)
    {
        _logger.LogCritical(message);
        HttpResponse response = httpContext.Response;
        response.ContentType = message;
        response.StatusCode = (int)httpStatusCode;

        var error = new
        {
            Message = message,
            StatusCode = (int)httpStatusCode
        };

        return await Task.FromResult(new BadRequestObjectResult(error));

    }
}


public static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionMiddleware>();
    }
}
