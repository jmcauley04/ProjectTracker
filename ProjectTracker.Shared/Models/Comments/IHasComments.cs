namespace ProjectTracker.Shared.Models.Comments;

public interface IHasComments
{
    IList<Comment> Comments { get; set; }
}
