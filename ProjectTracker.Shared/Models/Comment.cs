namespace ProjectTracker.Shared.Models;

public class Comment
{
    public int Id { get; set; }
    public int RelationId { get; set; }
    public int RelationTypeId { get; set; }
    public RelationType? RelationType { get; set; }
    public string User { get; set; }
    public string Text { get; set; }
    public DateTime Created { get; set; }
}
