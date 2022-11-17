using Microsoft.Extensions.DependencyInjection;
using ProjectTracker.Shared.Attributes;
using System.Reflection;

namespace ProjectTracker.Shared.Extensions;

public static class ServiceInjectionExtensions
{
    public static IServiceCollection AddAttributedProjectServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            var attr = type.GetCustomAttribute<InjectServiceAttribute>();
            if (attr is null)
                continue;

            switch (attr.Lifetime)
            {
                case InjectServiceAttribute.ServiceLifetime.Transiet:
                    services.AddTransient(type);
                    continue;
                case InjectServiceAttribute.ServiceLifetime.Scoped:
                    services.AddScoped(type);
                    continue;
                case InjectServiceAttribute.ServiceLifetime.Singleton:
                    services.AddSingleton(type);
                    continue;
            }
        }

        return services;
    }
}
