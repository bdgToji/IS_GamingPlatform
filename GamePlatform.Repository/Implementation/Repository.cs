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
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> enteties;
        string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            enteties = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return enteties.AsEnumerable();
        }

        public T Get(Guid? id)
        {
            return enteties.SingleOrDefault(s => s.Id == id);
        }

        public void Insert(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            enteties.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            enteties.Update(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            enteties.Remove(entity);
            context.SaveChanges();
        }
    }
}
