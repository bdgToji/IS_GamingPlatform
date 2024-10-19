namespace GamePlatform.Domain.Domain
{
    public class GameInOrder : BaseEntity
    {
        public Guid? GameId { get; set; }
        public Game? Game { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public int Quantity { get; set; }
    }
}
