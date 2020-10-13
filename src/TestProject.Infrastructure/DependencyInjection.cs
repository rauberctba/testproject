﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Application.Common.Interfaces;
using TestProject.Infrastructure.Database;

namespace TestProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));

            services.AddScoped<IDatabaseContext>(provider => provider.GetService<DatabaseContext>());
            return services;

        }
    }
}
