using Microsoft.Extensions.DependencyInjection;

namespace ProjectTracker.Shared;


public static class IServiceCollectionInjector
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        return services;
    }
}
