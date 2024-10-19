using GamePlatform.Domain;
using GamePlatform.Domain.Domain;
using GamePlatform.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.Repository
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Developer> Developers { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<GameInCart> GameInCarts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<GameInOrder> GameInOrders { get; set; }
        public virtual DbSet<EmailMessage> EmailMessages { get; set; }

    }
}
