using GamePlatform.Domain.Identity;

namespace GamePlatform.Domain.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public virtual ICollection<GameInCart>? GamesInCart { get; set; }
    }
}
