using ProjectTracker.Shared.Extensions;

namespace ProjectTracker.Shared;


public static class IServiceCollectionInjector
{
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        services.AddAttributedProjectServices();

        return services;
    }
}
