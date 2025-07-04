using ExpenseTracker.Application.Common.Interface;
using ExpenseTracker.Infrasturcture.Jwt;
using ExpenseTracker.Infrasturcture.Persistence;
using ExpenseTracker.Infrasturcture.Persistence.Configuration;
using ExpenseTracker.SharedKernel.Models.Common.Class;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrasturcture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)), ServiceLifetime.Scoped);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMemoryCache();
            services.Configure<JwtConfigDto>(configuration.GetSection("JwtAuth"));
            return services;
        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtTokenServices, JwtTokenServices>();
            return services;
        }
    }
}
