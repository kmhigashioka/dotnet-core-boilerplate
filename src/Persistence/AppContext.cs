using Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Persistence
{
    public class AppContext : IdentityDbContext<ApplicationUser>, IAppContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }

    public static class DbInitializer
    {
        public static void Initialize(IAppContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
