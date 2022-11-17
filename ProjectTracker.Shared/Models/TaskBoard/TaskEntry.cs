namespace ProjectTracker.Shared.Models.TaskBoard;

public class TaskEntry
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TaskStatus? Status { get; set; }
    public int StatusId { get; set; }
    //public User Owner { get; set; }
    public TaskPriority? Priority { get; set; }
    public int PriorityId { get; set; }
    public DateTime? Due { get; set; } = DateTime.Now;
    public DateTime Created { get; set; } = DateTime.Now;

    public TaskEntry Clone() => (TaskEntry)MemberwiseClone();
}