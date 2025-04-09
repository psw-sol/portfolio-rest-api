namespace LoginServer.Controllers
{
    using Common.Controller;
    using LoginServer.Data;
    using LoginServer.Data.Entities;
    using LoginServer.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Shared.Grpc;
    using Shared.Protos;
    using System.Security.Cryptography;
    using System.Text;

    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly MemberDbContext _db;
        private readonly JwtService _jwt;

        public AuthController(MemberDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        [HttpPost("login")]
        [Consumes("application/x-protobuf")]
        [Produces("application/x-protobuf")]
        public async Task<IActionResult> Login([FromBody] PLoginRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
            {
                user = new User
                {
                    Username = request.Username,
                    PasswordHash = HashPassword(request.Password)
                };
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
            }
            else if (user.PasswordHash != HashPassword(request.Password))
            {
                return Unauthorized(new { error = "Invalid password" });
            }

            var token = _jwt.GenerateToken(user.Id, user.Username);
            return Ok(new PLoginResponse{ Success = true, Token = token, Error = string.Empty});
        }

        private string HashPassword(string pw)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(pw)));
        }
    }

}
