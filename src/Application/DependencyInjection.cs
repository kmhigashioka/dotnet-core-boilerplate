using System.Reflection;
using Application.Common.Mediators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMediator, AppMediator>();
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
