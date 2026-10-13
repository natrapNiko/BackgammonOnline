using BackgammonOnline.Web.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackgammonOnline.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //DbSet
        public DbSet<GameRoom> GameRooms { get; set; }
        public DbSet<GameRoomMember> GameRoomMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GameRoomMember>()
                .HasIndex(m => new { m.GameRoomId, m.UserId })
                .IsUnique();
        }
    }
}
