using System;
using InfinityConn.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfinityConn.Infraestructure;

public static class InfraestructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services             
                // .AddServices()
                // .AddRepositories()
                .AddData(configuration);

            return services;
        }

        private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(o => 
            o.UseSqlServer(connectionString, sqlOptions => 
                sqlOptions.EnableRetryOnFailure()));

            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {

            return services;
        }
}
