using MarketingTgBot.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketingTgBot.Repositories;

public class UserRepository
{
    private readonly AppDbContext AppDbContext;

    private static long adminChatId;

    public UserRepository(AppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
    }

    public async Task<List<User>> GetUsers()
    {
        var users = AppDbContext.Users.ToList();
        return users;
    }

    public async Task<User> GetUserByChatId(long chatId)
    {
        var user = await AppDbContext.Users.FirstOrDefaultAsync(u => u.ChatId == chatId);
        return user;
    }

    public async Task Create(User user)
    {
        AppDbContext.Users.Add(user);
        await AppDbContext.SaveChangesAsync();
    }
}