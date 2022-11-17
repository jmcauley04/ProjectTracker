namespace ProjectTracker.Shared.Models;

public class ProjectEvent
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime TargetDate { get; set; } = DateTime.Now;
    public DateTime OriginalDate { get; set; } = DateTime.Now;
}
