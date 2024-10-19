using GamePlatform.Domain.Domain;
using GamePlatform.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }

        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.GamesInOrder)
                .Include(z => z.User)
                .Include("GamesInOrder.Game")
                .Include("GamesInOrder.Game.Developer")
                .ToList();
        }

        public Order GetOrderDetails(BaseEntity id)
        {
            return entities
                .Include(z => z.GamesInOrder)
                .Include(z => z.User)
                .Include("GamesInOrder.Game")
                .Include("GamesInOrder.Game.Developer")
                .SingleOrDefaultAsync(z => z.Id == id.Id).Result;
        }
    }
}
