using System;
using InfinityConn.Application.Commands.Register;
using Microsoft.Extensions.DependencyInjection;

namespace InfinityConn.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddHandlers();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblyContaining<RegisterCommand>());

            return services;
        }
}