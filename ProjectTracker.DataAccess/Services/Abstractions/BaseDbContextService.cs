using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ProjectTracker.DataAccess.Services.Abstractions;

public abstract class BaseDbContextService<TContext>
    where TContext : DbContext
{
    protected readonly IDbContextFactory<TContext> _dbContextFactory;
    protected readonly IMemoryCache _memoryCache;


    public BaseDbContextService(IDbContextFactory<TContext> dbContextFactory, IMemoryCache memoryCache)
    {
        _dbContextFactory = dbContextFactory;
        _memoryCache = memoryCache;
    }

    private async Task<TResult> WithLogging<TResult>(Func<TContext, Task<TResult>> func, bool saveChanges)
    {
        try
        {
            using var ctx = await _dbContextFactory.CreateDbContextAsync();
            var result = await func(ctx);
            if (saveChanges)
                await ctx.SaveChangesAsync();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    protected async Task<TReturn> ReadWithLogging<TReturn>(Func<TContext, Task<TReturn>> func) => await WithLogging(func, false);
    protected async Task<TReturn> ReadWithLogging_Cached<TReturn>(Func<TContext, Task<TReturn>> func, string key)
    {
        var result = await _memoryCache.GetOrCreateAsync(key, async x =>
        {
            x.SetAbsoluteExpiration(DateTimeOffset.UtcNow.AddMinutes(1));
            var result = await WithLogging(func, false);
            if (result is null)
                return default;
            x.SetValue(result);
            return result;
        });

        if (result is null) throw new Exception("Null result");

        return result;
    }
    protected async Task<TReturn> WriteWithLogging<TReturn>(Func<TContext, Task<TReturn>> func) => await WithLogging(func, true);

}
