using GameServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Data
{
    public class GameDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatus> PlayersStatuses { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Status)
                .WithOne(s => s.Player)
                .HasForeignKey<PlayerStatus>(s => s.PlayerId);
        }
    }

}
