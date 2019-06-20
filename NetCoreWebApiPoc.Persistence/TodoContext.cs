using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApiPoC.Application.Interfaces;
using NetCoreWebApiPoC.Domain.Entities;

namespace NetCoreWebApiPoC.Persistence
{
    public class TodoContext : IdentityDbContext<ApplicationUser>, ITodoContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }

    public static class DbInitializer
    {
        public static void Initialize(ITodoContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
