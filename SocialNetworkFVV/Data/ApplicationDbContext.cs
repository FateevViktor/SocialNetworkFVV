using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetworkFVV.Data.Configuration;
using SocialNetworkFVV.Models;
using System.Reflection.Emit;

namespace SocialNetworkFVV.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Message> Messages { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            
            builder.Entity<Friend>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friends)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ApplyConfiguration(new MessageConfuiguration());
            //builder.ApplyConfiguration<Friend>(new FriendConfiguration());
        }
    }
}
