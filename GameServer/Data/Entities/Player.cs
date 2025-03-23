namespace GameServer.Data.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public long UserId {  get; set; }
        public int ServerId { get; set; }
        public string Name { get; set; } = string.Empty;

        public PlayerStatus Status { get; set; } = null!;
    }
}
