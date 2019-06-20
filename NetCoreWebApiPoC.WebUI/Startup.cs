using System.Reflection;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreWebApiPoC.Application.Interfaces;
using NetCoreWebApiPoC.Application.Todos.Commands.NewTodo;
using NetCoreWebApiPoC.Domain.Entities;
using NetCoreWebApiPoC.Persistence;
using NetCoreWebApiPoC.WebUI.Configuration;

namespace NetCoreWebApiPoC.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();
            services.AddMediatR(typeof(NewTodoCommand).GetTypeInfo().Assembly);
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TodoContext>()
                .AddDefaultTokenProviders();
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<ProfileService>();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:9340";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                });
            services.AddScoped<ITodoContext>(provider => provider.GetService<TodoContext>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
            app.UseFileServer();
        }
    }
}
