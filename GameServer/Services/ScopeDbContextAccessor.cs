using GameServer.Data;

namespace GameServer.Services
{
    public interface IScopedDbContextAccessor
    {
        GameDbContext? DbContext { get; set; }
    }

    public class ScopedDbContextAccessor : IScopedDbContextAccessor
    {
        public GameDbContext? DbContext { get; set; }
    }

}
