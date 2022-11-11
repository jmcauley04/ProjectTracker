﻿#nullable disable
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Shared.Models;
using ProjectTracker.Shared.Models.Comments;
using ProjectTracker.Shared.Models.Punchlist;
using ProjectTracker.Shared.Models.TaskBoard;

namespace ProjectTracker.DataAccess.Contexts;

public class ProjectTrackerContext : DbContext
{
    internal DbSet<Comment> Comments { get; set; }

    #region Punchlist
    internal DbSet<PunchlistEntry> PunchlistEntries { get; set; }
    internal DbSet<PunchlistPriority> PunchlistPriorities { get; set; }
    internal DbSet<PunchlistStatus> PunchlistStatuses { get; set; }
    internal DbSet<PunchlistOwner> PunchlistOwners { get; set; }
    internal DbSet<PunchlistFlag> PunchlistFlags { get; set; }
    #endregion

    #region Tasks
    internal DbSet<TaskEntry> TaskEntries { get; set; }
    internal DbSet<TaskPriority> TaskPriorities { get; set; }
    internal DbSet<Shared.Models.TaskBoard.TaskStatus> TaskStatuses { get; set; }
    #endregion
    #region History
    internal DbSet<HistoryLog> HistoryLogs { get; set; }
    #endregion

    public ProjectTrackerContext(DbContextOptions<ProjectTrackerContext> options) : base(options)
    {

    }
}
