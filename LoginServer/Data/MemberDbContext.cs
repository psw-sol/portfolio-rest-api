using Common.Database;
using LoginServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoginServer.Data
{
    public class MemberDbContext : BaseDbContext
    {
        public MemberDbContext(DbContextOptions<MemberDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
