namespace ProjectTracker.Shared.Models.Punchlist;

public class PunchlistEntry
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Due { get; set; }
    public int StatusId { get; set; } = 1;
    public PunchlistStatus? Status { get; set; }
    public int OwnerId { get; set; } = 1;
    public PunchlistOwner? Owner { get; set; }
    public int FlagId { get; set; } = 1;
    public PunchlistFlag? Flag { get; set; }
    public int PriorityId { get; set; } = 1;
    public PunchlistPriority? Priority { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Resolution { get; set; }
    public string? BeforeImage { get; set; }
    public string? AfterImage { get; set; }
    public string? ApprovedBy { get; set; }
    public double? X { get; set; }
    public double? Y { get; set; }

    public PunchlistEntry Clone() => (PunchlistEntry)MemberwiseClone();
}
