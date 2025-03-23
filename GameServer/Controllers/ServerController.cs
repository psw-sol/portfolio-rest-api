namespace GameServer.Controllers
{
    using GameServer.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("v1/servers")]
    public class ServerController : ControllerBase
    {
        private readonly GlobalDbContext _globalDb;

        public ServerController(GlobalDbContext globalDb)
        {
            _globalDb = globalDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var servers = await _globalDb.Servers
                .Where(s => s.IsActive)
                .Select(s => new
                {
                    id = s.Id,
                    name = s.Name,
                    desc = s.Desc,
                    curPlayers= s.CurPlayers,
                    maxPlayers = s.MaxPlayers
                })
                .ToListAsync();

            return Ok(servers);
        }
    }
}
