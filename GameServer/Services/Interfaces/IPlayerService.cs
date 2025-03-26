using Shared.Protos;

namespace GameServer.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<PPlayerSelectResponse> PlayerSelectAsync(long userId, int serverId);
    }
}
