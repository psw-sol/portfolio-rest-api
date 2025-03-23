using Shared.Grpc;

namespace GameServer.Helpers
{
    public class AuthGrpcClient
    {
        private readonly Auth.AuthClient _client;

        public AuthGrpcClient(Auth.AuthClient client)
        {
            _client = client;
        }

        public async Task<int?> VerifyTokenAsync(string token)
        {
            var reply = await _client.VerifyTokenAsync(new TokenRequest { Token = token });
            return reply.Valid ? reply.UserId : null;
        }
    }
}
