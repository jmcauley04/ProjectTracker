using ProjectTracker.DataAccess.Services;
using ProjectTracker.Shared.Constants;
using ProjectTracker.Shared.Extensions;
using ProjectTracker.Shared.Models;
using ProjectTracker.Shared.Models.Punchlist;

namespace ProjectTracker.Controllers;

public class PunchlistController
{
    private readonly PunchlistService _punchlistService;
    private readonly HistoryLogService _historyLogService;
    private readonly OptionsService _optionsService;
    private readonly UserService _userService;
    private readonly CommentService _commentService;

    public PunchlistController(
        PunchlistService punchlistService,
        HistoryLogService historyLogService,
        OptionsService optionsService,
        UserService userService,
        CommentService commentService
        )
    {
        _punchlistService = punchlistService;
        _historyLogService = historyLogService;
        _optionsService = optionsService;
        _userService = userService;
        _commentService = commentService;
    }

    public async Task<PunchlistEntry> Save(PunchlistEntry entity, PunchlistEntry originalEntity)
    {
        if (entity.Id == 0)
            return await Add(entity);
        else
        {
            var relationId = await GetPunchlistRelationId();
            var user = await _userService.GetUsername();
            var changes = new List<HistoryLog>();
            changes.LoadChanges(entity, originalEntity, relationId, user);

            if (changes.Any())
                return await Update(entity, changes);

            return entity;
        }
    }

    public async Task<PunchlistEntry> Add(PunchlistEntry orig)
    {
        try
        {
            var relationId = await GetPunchlistRelationId();
            var user = await _userService.GetUsername();

            PrepForUpdate(orig, null);
            var entity = await _punchlistService.Create(orig);
            var historyLog = new HistoryLog()
                .Created(
                    entity.Id,
                    relationId,
                    user);
            await _historyLogService.Create(historyLog);
            entity = await _punchlistService.Get(entity.Id);
            PrepForUpdate(orig, entity);
            return orig;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    async Task<int> GetPunchlistRelationId() => (await _optionsService.GetRelation(RelationTypes.Punchlist)).Id;

    public async Task<PunchlistEntry> Update(PunchlistEntry orig, IEnumerable<HistoryLog> changes)
    {
        try
        {
            var relations = await _optionsService.GetRelations();

            PrepForUpdate(orig, null);
            var entity = await _punchlistService.Update(orig);
            await _historyLogService.Create(changes);
            entity = await _punchlistService.Get(entity.Id);
            PrepForUpdate(orig, entity);
            return orig;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<List<PunchlistEntry>> GetPunchlist()
    {
        try
        {
            return await _punchlistService.GetAll();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<List<Comment>> GetComments(int entityId)
    {
        var relationTypeId = await GetPunchlistRelationId();
        return await _commentService.GetSome(x => x.RelationId == entityId && x.RelationTypeId == relationTypeId);
    }

    public async Task<Comment> AddComment(string text, int entityId)
        => await _commentService.AddComment(text, await GetPunchlistRelationId(), entityId, await _userService.GetUsername());

    static void PrepForUpdate(PunchlistEntry entity, PunchlistEntry? other)
    {
        entity.Priority = other?.Priority;
        entity.Status = other?.Status;
        entity.Flag = other?.Flag;
        entity.Owner = other?.Owner;
    }
}
