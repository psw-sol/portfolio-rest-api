using Common.Database;
using Common.Helper;
using GameServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Data
{
    public class GameDbContext : BaseDbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatus> PlayersStatuses { get; set; }
        public DbSet<ReserveTask> ReserveTasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Status)
                .WithOne(s => s.Player)
                .HasForeignKey<PlayerStatus>(s => s.PlayerId);
        }

    }

}
