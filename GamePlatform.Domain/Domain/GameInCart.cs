namespace GamePlatform.Domain.Domain
{
    public class GameInCart : BaseEntity
    {
        public Guid? GameId { get; set; }
        public Game? Game { get; set; }
        public Guid? ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
