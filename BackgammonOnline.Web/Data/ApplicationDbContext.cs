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
        public DbSet<Game> Games { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //One room can contain many members
            builder.Entity<GameRoomMember>()
                .HasOne(m => m.GameRoom)
                .WithMany(r => r.Members)
                .HasForeignKey(m => m.GameRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            //One registered user can belong to meny rooms
            builder.Entity<GameRoomMember>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //Prevent duplicate memberships
            builder.Entity<GameRoomMember>()
                .HasIndex(m => new { m.GameRoomId, m.UserId })
                .IsUnique();

            //A Game can optionally belong to a room
            builder.Entity<Game>()
                .HasOne(g => g.GameRoom)
                .WithMany()
                .HasForeignKey(g => g.GameRoomId)
                .OnDelete(DeleteBehavior.Restrict);

            //First player
            builder.Entity<Game>()
                .HasOne(g => g.PlayerOne)
                .WithMany()
                .HasForeignKey(g => g.PlayerOneId)
                .OnDelete(DeleteBehavior.Restrict);

            //Second player
            builder.Entity<Game>()
                .HasOne(g => g.PlayerTwo)
                .WithMany()
                .HasForeignKey(g => g.PlayerTwoId)
                .OnDelete(DeleteBehavior.Restrict);

            //WInner
            builder.Entity<Game>()
                .HasOne(g => g.Winner)
                .WithMany()
                .HasForeignKey(g => g.WinnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
