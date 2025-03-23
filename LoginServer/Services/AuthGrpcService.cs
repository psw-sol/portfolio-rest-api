namespace LoginServer.Services
{
    using global::Grpc.Core;
    using Shared.Grpc;
    using System.Security.Claims;

    public class AuthGrpcService : Auth.AuthBase
    {
        private readonly JwtService _jwt;

        public AuthGrpcService(JwtService jwt)
        {
            _jwt = jwt;
        }

        public override Task<TokenResponse> VerifyToken(TokenRequest request, ServerCallContext context)
        {
            var principal = _jwt.ValidateToken(request.Token);

            if (principal == null)
            {
                return Task.FromResult(new TokenResponse
                {
                    Valid = false,
                    Error = "Invalid or expired token"
                });
            }

            var userId = principal.FindFirst("userId")?.Value;
            var username = principal.FindFirst("sub")?.Value;

            return Task.FromResult(new TokenResponse
            {
                Valid = true,
                UserId = int.Parse(userId),
                Username = username
            });
        }
    }

}
