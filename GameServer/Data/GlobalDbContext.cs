using Common.Database;
using GameServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Data
{
    public class GlobalDbContext : BaseDbContext
    {

        public GlobalDbContext(DbContextOptions<GlobalDbContext> options) : base(options) { }
        public DbSet<Server> Servers { get; set; }
    }
}

