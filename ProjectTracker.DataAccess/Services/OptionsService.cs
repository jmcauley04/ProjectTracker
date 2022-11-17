using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProjectTracker.DataAccess.Contexts;
using ProjectTracker.DataAccess.Services.Abstractions;
using ProjectTracker.Shared.Attributes;
using ProjectTracker.Shared.Models;
using ProjectTracker.Shared.Models.Punchlist;

namespace ProjectTracker.DataAccess.Services;

[InjectService(InjectServiceAttribute.ServiceLifetime.Scoped)]
public class OptionsService : BaseDbContextService<ProjectTrackerContext>
{
    public OptionsService(IDbContextFactory<ProjectTrackerContext> dbContextFactory, IMemoryCache memoryCache) : base(dbContextFactory, memoryCache)
    {
    }

    public async Task<List<TOption>> GetOptions<TOption>(string key) where TOption : class
        => await ReadWithLogging_Cached(async ctx => await ctx.Set<TOption>().ToListAsync(), key);

    public async Task<List<PunchlistStatus>> GetPunchlistStatuses() => await GetOptions<PunchlistStatus>("GetPunchlistStatuses");
    public async Task<List<PunchlistPriority>> GetPunchlistPriorities() => await GetOptions<PunchlistPriority>("GetPunchlistPriorities");
    public async Task<List<PunchlistOwner>> GetPunchlistOwners() => await GetOptions<PunchlistOwner>("GetPunchlistOwners");
    public async Task<List<PunchlistFlag>> GetPunchlistFlags() => await GetOptions<PunchlistFlag>("GetPunchlistFlags");
    public async Task<List<RelationType>> GetRelations() => await GetOptions<RelationType>("GetRelations");
    public async Task<RelationType> GetRelation(string relation) => (await GetRelations()).Single(x => x.Name == relation);
    public async Task<List<Shared.Models.TaskBoard.TaskStatus>> GetTaskStatuses() => await GetOptions<Shared.Models.TaskBoard.TaskStatus>("GetTaskStatuses");
    public async Task<List<Shared.Models.TaskBoard.TaskPriority>> GetTaskPriorities() => await GetOptions<Shared.Models.TaskBoard.TaskPriority>("GetTaskPriorities");
}
