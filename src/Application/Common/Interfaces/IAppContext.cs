using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces
{
    public interface IAppContext
    {
        DbSet<Todo> Todos { get; set; }
        int SaveChanges();
        DatabaseFacade Database { get; }
    }
}
