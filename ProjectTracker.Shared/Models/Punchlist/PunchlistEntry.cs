namespace ProjectTracker.Shared.Models.Punchlist;

public class PunchlistEntry
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Due { get; set; }
    public int StatusId { get; set; }
    public PunchlistStatus? Status { get; set; }
    public int OwnerId { get; set; }
    public PunchlistOwner? Owner { get; set; }
    public int FlagId { get; set; }
    public PunchlistFlag? Flag { get; set; }
    public int PriorityId { get; set; }
    public PunchlistPriority? Priority { get; set; }
    public string Description { get; set; }
    public string? Resolution { get; set; }
    public string? BeforeImage { get; set; }
    public string? AfterImage { get; set; }
    public double? X { get; set; }
    public double? Y { get; set; }
}
