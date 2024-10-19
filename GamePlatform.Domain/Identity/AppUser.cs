using GamePlatform.Domain.Domain;
using Microsoft.AspNetCore.Identity;

namespace GamePlatform.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public string? ProfileName { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
