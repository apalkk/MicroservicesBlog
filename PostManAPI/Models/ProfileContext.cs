namespace PostManAPI.Models;

using Microsoft.EntityFrameworkCore;


public class ProfileContext : DbContext
{
    public ProfileContext(DbContextOptions<ProfileContext> options)
        : base(options)
    {
    }

    public DbSet<Profile> ProfileItems { get; set; } = null!;
}