﻿using System;
using System.Reflection;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreWebApiPoC.Application.Interfaces;
using NetCoreWebApiPoC.Application.Todos.Commands.NewTodo;
using NetCoreWebApiPoC.Domain.Entities;
using NetCoreWebApiPoC.WebUI.Configuration;
using AppContext = NetCoreWebApiPoC.Persistence.AppContext;

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
            services.AddDbContext<AppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson();
            services.AddMediatR(typeof(NewTodoCommand).GetTypeInfo().Assembly);
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppContext>()
                .AddDefaultTokenProviders();
            services.AddMvcCore()
                .AddAuthorization();
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
            services.AddScoped<IAppContext>(provider => provider.GetService<AppContext>());
            services.AddSwaggerGen(provider => 
                provider.SwaggerDoc("latest", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Core API", Description = "Swagger Core API" }
            ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(p =>
            {
                p.SwaggerEndpoint("/swagger/latest/swagger.json", "default");
                p.RoutePrefix = string.Empty;
            });
            app.UseFileServer();
        }
    }
}