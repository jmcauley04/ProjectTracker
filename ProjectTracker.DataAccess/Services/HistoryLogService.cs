using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProjectTracker.DataAccess.Contexts;
using ProjectTracker.DataAccess.Services.Abstractions;
using ProjectTracker.Shared.Attributes;
using ProjectTracker.Shared.Models;

namespace ProjectTracker.DataAccess.Services;

[InjectService(InjectServiceAttribute.ServiceLifetime.Scoped)]
public class HistoryLogService : BaseDbEntityService<ProjectTrackerContext, HistoryLog>
{
    public HistoryLogService(IDbContextFactory<ProjectTrackerContext> dbContextFactory, IMemoryCache memoryCache) : base(dbContextFactory, memoryCache)
    {
    }
    protected override IQueryable<HistoryLog> BaseGetQuery(ProjectTrackerContext ctx) => ctx.HistoryLogs;

    public override async Task<HistoryLog> Get(int id) => await Get(id, x => x.Id == id);

}
