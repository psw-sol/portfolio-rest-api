namespace GameServer.Data.Entities
{
    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public string Desc { get; set; } = string.Empty;
        public string ShardConnectionString { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int MaxPlayers { get; set; }
        public int CurPlayers { get; set; }
    }
}
