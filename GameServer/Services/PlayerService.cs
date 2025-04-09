using GameServer.Data;
using GameServer.Data.Entities;
using GameServer.Helpers;
using GameServer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Protos;

namespace GameServer.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IScopedDbContextAccessor _dbAccessor;
        private readonly AuthGrpcClient _authGrpcClient;
        private readonly GlobalDbContext _globalDb;

        public PlayerService(
            IScopedDbContextAccessor dbAccessor,
            AuthGrpcClient authGrpcClient)
        {
            _dbAccessor = dbAccessor;
            _authGrpcClient = authGrpcClient;
        }

        public async Task<PPlayerSelectResponse> PlayerSelectAsync(long userId, int serverId)
        {

            var db = _dbAccessor.DbContext;
            if (db == null) throw new Exception("No DB context for ServerId");

            var player = await db.Players.FirstOrDefaultAsync(c => c.UserId == userId);

            if (player == null)
            {
                var server = await _globalDb.Servers.FirstOrDefaultAsync(s => s.Id == serverId);
                if (server == null)
                {
                    throw new Exception("Not Exist Server");
                }

                if (server.CurPlayers > server.MaxPlayers)
                {
                    throw new Exception("Too Many Players On Server");
                }

                player = new Player
                {
                    UserId = userId,
                    ServerId = serverId,
                    Name = $"User{userId}",
                    Status = new PlayerStatus { Level = 1, Exp = 0, JobId = 1 }
                };
                db.Players.Add(player);

                server.CurPlayers++;
            }

            var response = new PPlayerSelectResponse
            {
                PlayerId = player.Id,
                Name = player.Name,
                Status = new PPlayerStatusInfo { Level = player.Status.Level, Exp = player.Status.Exp, JobId = player.Status.JobId }
            };

            return response;
        }
    }
}
