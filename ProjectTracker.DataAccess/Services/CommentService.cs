using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProjectTracker.DataAccess.Contexts;
using ProjectTracker.DataAccess.Services.Abstractions;
using ProjectTracker.Shared.Attributes;
using ProjectTracker.Shared.Models;

namespace ProjectTracker.DataAccess.Services;

[InjectService(InjectServiceAttribute.ServiceLifetime.Scoped)]
public class CommentService : BaseDbEntityService<ProjectTrackerContext, Comment>
{
    public CommentService(
        IDbContextFactory<ProjectTrackerContext> dbContextFactory, IMemoryCache memoryCache) : base(dbContextFactory, memoryCache)
    {
    }

    public override async Task<Comment> Get(int id) => await Get(id, x => x.Id == id);

    protected override IQueryable<Comment> BaseGetQuery(ProjectTrackerContext ctx) => ctx.Comments;

    public async Task<Comment> AddComment(string text, int relationTypeId, int entityId, string user)
    {
        var comment = new Comment()
        {
            Created = DateTime.Now,
            RelationId = entityId,
            RelationTypeId = relationTypeId,
            Text = text,
            User = user
        };

        return await Create(comment);
    }
}
