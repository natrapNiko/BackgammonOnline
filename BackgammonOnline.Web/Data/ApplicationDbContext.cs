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
        public DbSet<GameMove> GameMoves { get; set; } = null!;
        public DbSet<GameTurn> GameTurns { get; set; } = null!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = null!;
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

            //Winner
            builder.Entity<Game>()
                .HasOne(g => g.Winner)
                .WithMany()
                .HasForeignKey(g => g.WinnerId)
                .OnDelete(DeleteBehavior.Restrict);

            //One game can contain many moves.
            builder.Entity<GameMove>()
                .HasOne(m => m.Game)
                .WithMany(g => g.Moves)
                .HasForeignKey(m => m.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            //One turn can contain many moves.
            builder.Entity<GameMove>()
                .HasOne(m => m.GameTurn)
                .WithMany(t => t.Moves)
                .HasForeignKey(m => m.GameTurnId)
                .OnDelete(DeleteBehavior.NoAction);

            //Prevent duplicate move numbers within the same turn
            builder.Entity<GameMove>()
                .HasIndex(m => new
                {
                    m.GameId,
                    m.TurnNumber,
                    m.MoveNumber
                })
                .IsUnique();

            //One Game can contain many turns
            builder.Entity<GameTurn>()
                .HasOne(t => t.Game)
                .WithMany(g => g.Turns)
                .HasForeignKey(t => t.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            //Each turn number must be unique within its game
            builder.Entity<GameTurn>()
                .HasIndex(t => new { t.GameId, t.TurnNumber })
                .IsUnique();

            //One room can contain many chat messages
            builder.Entity<ChatMessage>()
                .HasOne(m => m.GameRoom)
                .WithMany(r => r.ChatMessages)
                .HasForeignKey(m => m.GameRoomId)
                .OnDelete(DeleteBehavior.Cascade);

            //one registered user can send many chat messages
            builder.Entity<ChatMessage>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            //Improve performance when retrieving room chat hostory
            builder.Entity<ChatMessage>()
                .HasIndex(m => new {m.GameRoomId, m.SendAtUtc});
        }
    }
}
