using Common.Controller;
using GameServer.Data;
using GameServer.Data.Entities;
using GameServer.Helpers;
using GameServer.Services;
using GameServer.Services.Interfaces;
using GameServer.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Protos;

namespace GameServer.Controllers
{
    [ApiController]
    [Route("v1/player")]
    public class PlayerController : BaseController
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [TokenAuthorize]
        [HttpPost("enter")]
        [Consumes("application/x-protobuf")]
        [Produces("application/x-protobuf")]
        public async Task<IActionResult> Enter([FromBody] PPlayerSelectRequest request)
        {
            int userId = (int)HttpContext.Items["UserId"];
            int serverId = (int)HttpContext.Items["ServerId"];

            var result = await RunInTransactionAsync(serverId, async () =>
            {
                return await _playerService.PlayerSelectAsync(userId, serverId);
            });

            return Ok(result);
        }
    }
}
