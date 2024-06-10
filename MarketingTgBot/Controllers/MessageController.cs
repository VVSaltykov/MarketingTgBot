using MarketingTgBot.Repositories;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBotLib.Core;
using TgBotLib.Core.Base;

namespace MarketingTgBot.Controllers;

public class MessageController : BotController
{
    private readonly IInlineButtonsGenerationService _buttonsGenerationService;
    private readonly IKeyboardButtonsGenerationService _keyboardButtonsGenerationService;
    private readonly UserRepository UserRepository;
    
    private static long adminChatId = 1740369388;

    public MessageController(IInlineButtonsGenerationService buttonsGenerationService, IKeyboardButtonsGenerationService keyboardButtonsGenerationService,
        UserRepository userRepository)
    {
        _buttonsGenerationService = buttonsGenerationService;
        _keyboardButtonsGenerationService = keyboardButtonsGenerationService;
        UserRepository = userRepository;
    }
    
    [UnknownMessage]
    public async Task SendMessage()
    {
        var chatId = Update.GetChatId();
        var users = await UserRepository.GetUsers();

        users.RemoveAll(u => u.ChatId == adminChatId);
        
        if (chatId == adminChatId)
        {
            var photos = Update.Message.Photo;
            
            foreach (var user in users)
            {
                if (photos == null)
                {
                    var messageText = Update.Message.Text;
                    if (!string.IsNullOrEmpty(messageText))
                    {
                        await Client.SendTextMessageAsync(user.ChatId, messageText);
                    }
                }
                else
                {
                    var photo = new InputFileId(photos[0].FileId);
                    var messageText = Update.Message.Caption;
                    await Client.SendPhotoAsync(user.ChatId, photo, caption: messageText);
                }
            }
        }
    }
}