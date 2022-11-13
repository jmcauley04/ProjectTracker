namespace ProjectTracker.Shared.Models;

public class HistoryLog
{
    public int Id { get; set; }
    public int RelationId { get; set; }
    public int RelationTypeId { get; set; }
    public RelationType? RelationType { get; set; }
    public string User { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string Description { get; set; } = string.Empty;
    public string Old { get; set; } = string.Empty;
    public string New { get; set; } = string.Empty;
}
