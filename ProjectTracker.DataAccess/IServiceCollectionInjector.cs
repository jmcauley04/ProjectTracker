using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectTracker.DataAccess.Contexts;
using ProjectTracker.DataAccess.Services;

namespace ProjectTracker.DataAccess;

public static class IServiceCollectionInjector
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {

#if RELEASE
        var connStr = Environment.GetEnvironmentVariable("connectionstring");
#else
        var connStr = configuration.GetConnectionString("ProjectTracker");
#endif

        services.AddDbContext<ProjectTrackerContext>(options =>
            options.UseSqlServer(connStr));

        services.AddDbContextFactory<ProjectTrackerContext>(lifetime: ServiceLifetime.Scoped);
        services.AddMemoryCache();

        services.AddScoped<TaskService>();
        services.AddScoped<PunchlistService>();
        services.AddScoped<OptionsService>();
        services.AddScoped<HistoryLogService>();
        services.AddScoped<CommentService>();

        return services;
    }
}
