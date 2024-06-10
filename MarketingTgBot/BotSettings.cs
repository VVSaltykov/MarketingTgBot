namespace MarketingTgBot;

public static class BotSettings
{
    public static string BotToken { get; } = Environment.GetEnvironmentVariable("BOT_TOKEN") ?? "7409576127:AAHK7OkijODHcp2ogEbJKjWj0QuNa_US0nw";
}