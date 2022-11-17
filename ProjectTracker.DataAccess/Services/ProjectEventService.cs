using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProjectTracker.DataAccess.Contexts;
using ProjectTracker.DataAccess.Services.Abstractions;
using ProjectTracker.Shared.Attributes;
using ProjectTracker.Shared.Models;

namespace ProjectTracker.DataAccess.Services;

[InjectService(InjectServiceAttribute.ServiceLifetime.Scoped)]
public class ProjectEventService : BaseDbEntityService<ProjectTrackerContext, ProjectEvent>
{
    public ProjectEventService(IDbContextFactory<ProjectTrackerContext> dbContextFactory, IMemoryCache memoryCache) : base(dbContextFactory, memoryCache)
    {
    }

    public override Task<ProjectEvent> Get(int id) => Get(id, x => x.Id == id);

    protected override IQueryable<ProjectEvent> BaseGetQuery(ProjectTrackerContext ctx) => ctx.ProjectEvents;
}
