using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using CommentPost.Telegram;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var telegramSettings = configuration.GetSection("TelegramBotSettings").Get<TelegramBotSettings>();
        services.AddSingleton(new TelegramServices(telegramSettings.BotToken, telegramSettings.ChatId));
        services.AddScoped<IHashPassword, HashPassword>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

       

        return services;
    }
}
