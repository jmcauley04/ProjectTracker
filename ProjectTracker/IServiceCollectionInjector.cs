namespace ProjectTracker.Shared;


public static class IServiceCollectionInjector
{
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        services.AddScoped<PunchlistController>();
        services.AddSingleton<FileService>();
        services.AddScoped<UserService>();

        return services;
    }
}
