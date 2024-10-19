using GamePlatform.Domain.Domain;
using GamePlatform.Domain.Identity;
using GamePlatform.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<AppUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<AppUser>();
        }

        public IEnumerable<AppUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public AppUser Get(string? id)
        {
            return entities
                .Include(z => z.ShoppingCart)
                .Include("ShoppingCart.GamesInCart")
                .Include("ShoppingCart.GamesInCart.Game")
                .SingleOrDefault(s => s.Id == id);
        }

        public void Insert(AppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(AppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(AppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
