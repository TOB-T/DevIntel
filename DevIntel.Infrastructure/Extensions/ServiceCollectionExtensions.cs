using DevIntel.Application.Interfaces;
using DevIntel.Infrastructure.Persistence.Context;
using DevIntel.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DevIntel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DevIntel.Infrastructure.Authentication;

namespace DevIntel.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Later you can register other things like:
            // - Repositories
            // - Logging
            // - Identity/Auth services

            services.AddScoped<IAuthService, AuthService>();
            // Register your JWT token generator
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole(Role.Admin.ToString()));
                options.AddPolicy("UserOnly", policy => policy.RequireRole(Role.User.ToString()));
            });

            return services;
        }

      

    }
}
