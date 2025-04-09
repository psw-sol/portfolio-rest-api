namespace GameServer.Data.Entities
{
    public class ReserveTask
    {
        public long Id {  get; set; }
        public long PlayerId {  get; set; }
        public int ServerId { get; set; }
        public long BehaviorId {  get; set; }
        public long Value { get; set; }
        public long RefValue {  get; set; }
    }
}
