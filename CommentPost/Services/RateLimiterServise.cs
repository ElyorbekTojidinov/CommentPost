using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace CommentPost.Services;

public static class RateLimiterServise
{
    public static IServiceCollection AddRateLimiterServise(this IServiceCollection services)
    { 
        services.AddRateLimiter(options =>
        {
            ////Fixed window limiter
            //options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            //{
            //    return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
            //        new FixedWindowRateLimiterOptions
            //        {
            //            PermitLimit = 5,
            //            AutoReplenishment = true,
            //            Window = TimeSpan.FromSeconds(10)
            //        });
            //});

            //// Concurrency limiter
            //options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            //{
            //    return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(), partition =>
            //        new FixedWindowRateLimiterOptions
            //        {
            //            PermitLimit =10,
            //            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            //            QueueLimit = 3
            //        });
            //});


            // Sliding window limiter
            options.AddSlidingWindowLimiter("SlidingLimiter", option =>
            {
                option.Window = TimeSpan.FromSeconds(10);
                option.QueueLimit = 0;
                option.PermitLimit = 5;
                option.AutoReplenishment = true;
                option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                option.SegmentsPerWindow = 4;
            });


            //Token bucket limiter
            options.AddTokenBucketLimiter("TokenBucketLimiter", option =>
            {
                option.TokenLimit = 5;
                option.QueueLimit = 0;
                option.AutoReplenishment = true;
                option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                option.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
                option.TokensPerPeriod = 1;
            });


            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.HttpContext.Response.WriteAsync("Too many requests. Please try later again... ", cancellationToken: token);
            };
        });


        return services;
    }
}
