using CQRS.Domain.Rents;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddTransient<CostService>();

        return services;
    }
}