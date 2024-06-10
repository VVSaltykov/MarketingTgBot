using MarketingTgBot.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketingTgBot;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}