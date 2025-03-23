using GameServer.Data;
using GameServer.Services;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Middleware
{
    public class DbContextResolverMiddleware
    {
        private readonly RequestDelegate _next;

        public DbContextResolverMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IScopedDbContextAccessor accessor, GlobalDbContext globalDb)
        {
            if (context.Request.Headers.TryGetValue("X-Server-Id", out var serverIdStr) &&
                int.TryParse(serverIdStr, out int serverId))
            {
                var serverInfo = await globalDb.Servers.FindAsync(serverId);
                if (serverInfo != null)
                {
                    var options = new DbContextOptionsBuilder<GameDbContext>()
                        .UseMySql(serverInfo.ShardConnectionString, ServerVersion.AutoDetect(serverInfo.ShardConnectionString))
                        .Options;

                    accessor.DbContext = new GameDbContext(options);
                }

                context.Items["ServerId"] = serverId;
            }

            await _next(context);
        }
    }

}
