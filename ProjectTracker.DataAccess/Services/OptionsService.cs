using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProjectTracker.DataAccess.Contexts;
using ProjectTracker.DataAccess.Services.Abstractions;
using ProjectTracker.Shared.Models;
using ProjectTracker.Shared.Models.Punchlist;

namespace ProjectTracker.DataAccess.Services
{
    public class OptionsService : BaseDbContextService<ProjectTrackerContext>
    {
        public OptionsService(IDbContextFactory<ProjectTrackerContext> dbContextFactory, IMemoryCache memoryCache) : base(dbContextFactory, memoryCache)
        {
        }

        public async Task<List<TOption>> GetOptions<TOption>(string key) where TOption : class
            => await ReadWithLogging_Cached(async ctx => await ctx.Set<TOption>().ToListAsync(), key);

        public async Task<List<PunchlistStatus>> GetStatuses() => await GetOptions<PunchlistStatus>("GetStatuses");
        public async Task<List<PunchlistPriority>> GetPriorities() => await GetOptions<PunchlistPriority>("GetPriorities");
        public async Task<List<PunchlistOwner>> GetOwners() => await GetOptions<PunchlistOwner>("GetOwners");
        public async Task<List<PunchlistFlag>> GetFlags() => await GetOptions<PunchlistFlag>("GetFlags");
        public async Task<List<RelationType>> GetRelations() => await GetOptions<RelationType>("GetRelations");
    }
}
