using GameServer.Data.Entities;

namespace GameServer.Services
{
    public class ReserveService
    {
        private readonly IScopedDbContextAccessor _dbAccessor;

        public void SaveReserve(ReserveTask reserveTask)
        {
            var db = _dbAccessor.DbContext;
            if (db == null) throw new Exception("No DB context for ServerId");

            db.ReserveTasks.Add(reserveTask);
        }
    }
}
