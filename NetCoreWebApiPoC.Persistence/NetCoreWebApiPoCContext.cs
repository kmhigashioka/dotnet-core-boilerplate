using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApiPoC.Application.Interfaces;
using NetCoreWebApiPoC.Domain.Entities;

namespace NetCoreWebApiPoC.Persistence
{
    public class NetCoreWebApiPoCContext : IdentityDbContext<ApplicationUser>, INetCoreWebApiPoCContext
    {
        public NetCoreWebApiPoCContext(DbContextOptions<NetCoreWebApiPoCContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }

    public static class DbInitializer
    {
        public static void Initialize(INetCoreWebApiPoCContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
