namespace ProjectTracker.Shared.Models.Comments;

public class Comment
{
    public int Id { get; set; }
    public int RelationTypeId { get; set; }
    public RelationType? RelationType { get; set; }
    public string User { get; set; }
    public string Text { get; set; }
    public DateTime Created { get; set; }
}
