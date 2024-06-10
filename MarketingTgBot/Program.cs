using MarketingTgBot.Repositories;
using Microsoft.EntityFrameworkCore;
using TgBotLib.Core;

namespace MarketingTgBot;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));
        });
        
        builder.Services.AddBotLibCore(BotSettings.BotToken);

        builder.Services.AddTransient<UserRepository>();
        
        var app = builder.Build();

        //app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}