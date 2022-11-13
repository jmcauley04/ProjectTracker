using ProjectTracker.Shared.Models;
using ProjectTracker.Shared.Models.Punchlist;

namespace ProjectTracker.Shared.Extensions;

public static class HistoryLogExtensions
{
    public static HistoryLog Created(this HistoryLog log, int relationId, int relationTypeId, string user)
    {
        log.Description = $"Created entry";
        log.RelationId = relationId;
        log.RelationTypeId = relationTypeId;
        log.Timestamp = DateTime.Now;
        log.User = user;
        return log;
    }

    public static HistoryLog Changed(this HistoryLog log, int relationId, string description, int relationTypeId, string user, string oldValue, string newValue, DateTime? timestamp = null)
    {
        log.Description = description;
        log.RelationId = relationId;
        log.RelationTypeId = relationTypeId;
        log.Timestamp = timestamp ?? DateTime.Now;
        log.User = user;
        log.Old = oldValue;
        log.New = newValue;
        return log;
    }

    public static IEnumerable<HistoryLog> LoadChanges(this List<HistoryLog> changes, PunchlistEntry newEntity, PunchlistEntry oldEntity, int relationTypeId, string user)
    {
        var timestamp = DateTime.Now;
        changes.Clear();

        void evaluateSelectBox(string name, Func<PunchlistEntry, int> getId, Func<PunchlistEntry, string?> getName)
        {
            if (getId(newEntity) != getId(oldEntity))
                changes.Add(new HistoryLog().Changed(
                    newEntity.Id,
                    $"Updated {name}",
                    relationTypeId,
                    user,
                    getName(oldEntity) ?? getId(oldEntity).ToString(),
                    getName(newEntity) ?? getId(newEntity).ToString(),
                    timestamp)
                );
        }

        void evaluateString(string name, Func<PunchlistEntry, string?> getValue)
        {
            if (getValue(newEntity) != getValue(oldEntity))
                changes.Add(new HistoryLog().Changed(
                    newEntity.Id,
                    $"Updated {name}",
                    relationTypeId,
                    user,
                    getValue(oldEntity) ?? string.Empty,
                    getValue(newEntity) ?? string.Empty,
                    timestamp)
                );
        }

        evaluateSelectBox(nameof(newEntity.Priority), x => x.PriorityId, x => x.Priority?.Name);
        evaluateSelectBox(nameof(newEntity.Status), x => x.StatusId, x => x.Status?.Name);
        evaluateSelectBox(nameof(newEntity.Flag), x => x.FlagId, x => x.Flag?.Name);
        evaluateSelectBox(nameof(newEntity.Owner), x => x.OwnerId, x => x.Owner?.Name);

        evaluateString(nameof(newEntity.ApprovedBy), x => x.ApprovedBy);
        evaluateString(nameof(newEntity.Name), x => x.Name);
        evaluateString(nameof(newEntity.Description), x => x.Description);
        evaluateString(nameof(newEntity.Resolution), x => x.Resolution);
        evaluateString(nameof(newEntity.BeforeImage), x => x.BeforeImage);
        evaluateString(nameof(newEntity.AfterImage), x => x.AfterImage);

        if ((newEntity.X, newEntity.Y) != (oldEntity.X, oldEntity.Y))
        {
            changes.Add(new HistoryLog().Changed(
            newEntity.Id,
                    $"Updated map",
                    relationTypeId,
                    user,
                    (oldEntity.X, oldEntity.Y).ToString(),
                    (newEntity.X, newEntity.Y).ToString(),
                    timestamp)
                );
        }

        return changes;
    }
}
