using Application;
using CommentPost.Middelwares;
using CommentPost.Services;
using Infrastructure;
using Serilog;
using Serilog.Events;
using TelegramSink;

namespace CommentPost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                  .ReadFrom.Configuration(builder.Configuration)
                  .Enrich.FromLogContext()
                  .WriteTo.TeleSink(
                  telegramApiKey: "telegramApiKey",
                  telegramChatId: "telegramChatId",
                  minimumLevel: LogEventLevel.Error)
                  .CreateLogger();


            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddCurrentUser();

            builder.Services.AddControllers();

            //builder.Services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = builder.Configuration.GetConnectionString("RedisDB");
            //});

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddLazyCache();

           

            var app = builder.Build();

            app.UseRateLimiter();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DisplayRequestDuration();
                });
            }

            app.UseHttpsRedirection();

            app.UseGlobalExceptionMiddleware();
            app.UseETagger();

            app.UseAuthorization();

            app.UseResponseCaching();

            app.MapControllers();

            app.Run();
        }
    }
}