using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProjectTracker.DataAccess.Contexts;
using ProjectTracker.DataAccess.Services.Abstractions;
using ProjectTracker.Shared.Models.Punchlist;

namespace ProjectTracker.DataAccess.Services;

public class PunchlistService : BaseDbEntityService<ProjectTrackerContext, PunchlistEntry>
{
    public PunchlistService(IDbContextFactory<ProjectTrackerContext> dbContextFactory, IMemoryCache memoryCache) : base(dbContextFactory, memoryCache)
    {

    }

    protected override IQueryable<PunchlistEntry> BaseGetQuery(ProjectTrackerContext ctx)
        => ctx.PunchlistEntries
            .Include(x => x.Status)
            .Include(x => x.Priority);

    public override async Task<PunchlistEntry> Get(int id) => await Get(id, x => x.Id == id);
}
