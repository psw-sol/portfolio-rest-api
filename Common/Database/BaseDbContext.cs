using Common.Helper;
using Microsoft.EntityFrameworkCore;

namespace Common.Database;

public abstract class BaseDbContext : DbContext, ITransactionalDbContext
{
    protected BaseDbContext(DbContextOptions options) : base(options) { }

    public bool HasChanges() => ChangeTracker.HasChanges();
}
