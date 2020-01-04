using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(IAppContext context)
        {
            context.Database.Migrate();
        }
    }
}
