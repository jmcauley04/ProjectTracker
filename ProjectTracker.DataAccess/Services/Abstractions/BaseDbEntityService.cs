using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace ProjectTracker.DataAccess.Services.Abstractions;

public abstract class BaseDbEntityService<TContext, TEntity> : BaseDbContextService<TContext>
    where TContext : DbContext
    where TEntity : class
{
    protected BaseDbEntityService(IDbContextFactory<TContext> dbContextFactory, IMemoryCache memoryCache) : base(dbContextFactory, memoryCache)
    {
    }

    protected abstract IQueryable<TEntity> BaseGetQuery(TContext ctx);
    protected KeyNotFoundException KeyNotFoundException(int id) => new($"Type of '{typeof(TEntity)}' not found with key '{id}'.");

    public abstract Task<TEntity> Get(int id);
    protected async Task<TEntity> Get(int id, Expression<Func<TEntity, bool>> primaryKeyExpression)
    {
        return await ReadWithLogging(async (ctx) =>
        {
            var result = await BaseGetQuery(ctx)
                .SingleOrDefaultAsync(primaryKeyExpression);

            if (result is null)
                throw KeyNotFoundException(id);

            return result;
        });
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await ReadWithLogging(async (ctx) =>
        {
            var result = await BaseGetQuery(ctx)
                .ToListAsync();

            return result;
        });
    }

    public async Task<List<TEntity>> GetSome(Expression<Func<TEntity, bool>> expression)
    {
        return await ReadWithLogging(async (ctx) =>
        {
            var result = await BaseGetQuery(ctx)
                .Where(expression)
                .ToListAsync();

            return result;
        });
    }

    public async Task<IEnumerable<TEntity>> Create(IEnumerable<TEntity> entities)
    {
        return await WriteWithLogging(async (ctx) =>
        {
            if (entities.IsNullOrEmpty()) throw new ArgumentNullException(nameof(entities));
            ctx.AddRange(entities);
            return await Task.FromResult(entities);
        });
    }

    public async Task<TEntity> Create(TEntity entity)
    {
        return await WriteWithLogging(async (ctx) =>
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            ctx.Add(entity);
            return await Task.FromResult(entity);
        });
    }

    public async Task<TEntity> Delete(TEntity entity)
    {
        return await WriteWithLogging(async (ctx) =>
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            ctx.Remove(entity);
            return await Task.FromResult(entity);
        });
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        return await WriteWithLogging(async (ctx) =>
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            ctx.Update(entity);
            return await Task.FromResult(entity);
        });
    }

    public async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities)
    {
        return await WriteWithLogging(async (ctx) =>
        {
            if (entities.IsNullOrEmpty()) throw new ArgumentNullException(nameof(entities));
            ctx.UpdateRange(entities);
            return await Task.FromResult(entities);
        });
    }
}
