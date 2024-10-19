using GamePlatform.Domain.Identity;

namespace GamePlatform.Domain.Domain
{
    public class Order : BaseEntity
    {
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public IEnumerable<GameInOrder>? GamesInOrder { get; set; }
    }
}
