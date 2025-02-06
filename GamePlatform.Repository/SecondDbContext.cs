using GamePlatform.Domain.Domain;
using GamePlatform.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePlatform.Repository
{
    public class SecondDbContext: IdentityDbContext<AppUser>
    {
        public SecondDbContext(DbContextOptions<SecondDbContext> options): base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
