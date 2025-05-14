using DevIntel.Domain.Entities;
using DevIntel.Domain.Enums;
using DevIntel.Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIntel.Infrastructure.Persistence.Seeders
{
    public static class SeedData
    {
        public static async Task SeedAdminAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!context.Users.Any(u => u.Role == Role.Admin))
            {
                var admin = new User
                {
                    Username = "admin",
                    Email = "admin@devintel.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = Role.Admin // Assuming you're using enums
                };

                context.Users.Add(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}
