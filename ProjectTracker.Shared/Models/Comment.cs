namespace ProjectTracker.Shared.Models;

public class Comment
{
    public int Id { get; set; }
    public int RelationId { get; set; }
    public int RelationTypeId { get; set; }
    public RelationType? RelationType { get; set; }
    public string User { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}
