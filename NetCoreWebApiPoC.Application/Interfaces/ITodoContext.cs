using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NetCoreWebApiPoC.Domain.Entities;

namespace NetCoreWebApiPoC.Application.Interfaces
{
    public interface ITodoContext
    {
        DbSet<Todo> Todos { get; set; }
        int SaveChanges();
        DatabaseFacade Database { get; }
    }
}
