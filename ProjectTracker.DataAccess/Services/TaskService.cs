using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProjectTracker.DataAccess.Contexts;
using ProjectTracker.DataAccess.Services.Abstractions;
using ProjectTracker.Shared.Models.TaskBoard;

namespace ProjectTracker.DataAccess.Services;

public class TaskService : BaseDbEntityService<ProjectTrackerContext, TaskEntry>
{
    public TaskService(IDbContextFactory<ProjectTrackerContext> dbContextFactory, IMemoryCache memoryCache) : base(dbContextFactory, memoryCache)
    {
    }

    protected override IQueryable<TaskEntry> BaseGetQuery(ProjectTrackerContext ctx)
        => ctx.TaskEntries
            .Include(x => x.Status)
            .Include(x => x.Priority);

    public override async Task<TaskEntry> Get(int id) => await Get(id, x => x.Id == id);
}
