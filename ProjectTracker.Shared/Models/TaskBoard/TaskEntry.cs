namespace ProjectTracker.Shared.Models.TaskBoard;

public class TaskEntry
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TaskStatus? Status { get; set; }
    public int StatusId { get; set; }
    //public User Owner { get; set; }
    public TaskPriority? Priority { get; set; }
    public int PriorityId { get; set; }
    public DateTime? Due { get; set; }
    public DateTime Created { get; set; }
}