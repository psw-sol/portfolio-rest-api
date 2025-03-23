using GameServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Data
{
    public class GlobalDbContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }

        public GlobalDbContext(DbContextOptions<GlobalDbContext> options) : base(options) { }
    }
}

