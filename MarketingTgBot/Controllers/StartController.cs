using MarketingTgBot.Models;
using MarketingTgBot.Repositories;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TgBotLib.Core;
using TgBotLib.Core.Base;

namespace MarketingTgBot.Controllers;

public class AccountController : BotController
{
    private readonly IUsersActionsService _usersActionsService;
    private readonly IInlineButtonsGenerationService _buttonsGenerationService;
    private readonly UserRepository UserRepository;

    private static User? User;
    private static string phoneNumber;
    private static string userName;

    public AccountController(IUsersActionsService usersActionsService, IInlineButtonsGenerationService buttonsGenerationService,
        UserRepository userRepository)
    {
        _usersActionsService = usersActionsService;
        _buttonsGenerationService = buttonsGenerationService;
        UserRepository = userRepository;
    }
    
    [Message("/start")]
    public async Task InitHandling()
    {
        try
        {
            User = await UserRepository.GetUserByChatId(BotContext.Update.GetChatId());
            if (User == null)
            {
                User user = new User
                {
                    ChatId = BotContext.Update.GetChatId()
                };
                await UserRepository.Create(user);
                _usersActionsService.HandleUser(BotContext.Update.GetChatId(), nameof(AccountController));
                await Client.SendTextMessageAsync(BotContext.Update.GetChatId(), "Отправьте любое текстовое сообщение для подтверждения, что Вы не робот \ud83e\udd16");
            }
        }
        catch (Exception ex)
        {
            // Обработка других исключений
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    
    [ActionStep(nameof(AccountController), 0)]
    public async Task FirstStep()
    {
        
        await Client.SendTextMessageAsync(BotContext.Update.GetChatId(), $"Подтверждение пройдено успешно \u2705\n\nСсылка на весь ассортимент Dyson по самым низким ценам \ud83d\udc49 https://t.me/+ZNcTMU3oQeAwZTZi\n\nВ честь регистрации мы дарим  вам скидку в 1000 рублей по промокоду«BOT1000»");
    }
}