using Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Database
{
    public static class AutoUnifiedTransactionHelper
    {
        public static async Task RunAsync(
            IServiceProvider provider,
            int serverId,
            Func<Task> action)
        {
            var dbs = provider.GetServices<ITransactionalDbContext>()
                .OfType<DbContext>()
                .ToList();

            if (dbs.Count == 0)
                throw new InvalidOperationException("No DbContexts found");

            var primary = dbs[0];
            await using var tx = await primary.Database.BeginTransactionAsync();

            foreach (var db in dbs.Skip(1))
                db.Database.UseTransaction(tx.GetDbTransaction());

            try
            {
                await action();

                var changed = dbs
                    .OfType<ITransactionalDbContext>()
                    .Where(d => d.HasChanges())
                    .OfType<DbContext>();

                foreach (var db in changed)
                    await db.SaveChangesAsync();

                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }

}
