namespace GameServer.Data.Entities
{
    public class PlayerStatus
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public int Level { get; set; }
        public long Exp { get; set; }
        public int JobId {  get; set; }

        public Player Player { get; set; } = null!;
    }
}
