using ProjectTracker.DataAccess.Services;
using ProjectTracker.Shared.Attributes;
using ProjectTracker.Shared.Models.TaskBoard;

namespace ProjectTracker.Controllers;

[InjectService(InjectServiceAttribute.ServiceLifetime.Scoped)]
public class TasksController
{
    private readonly TaskService _taskService;
    private readonly OptionsService _optionsService;

    public TasksController(TaskService taskService, OptionsService optionsService)
    {
        _taskService = taskService;
        _optionsService = optionsService;
    }

    public async Task<List<TaskEntry>> GetTasks() => await _taskService.GetAll();

    public async Task<IEnumerable<Shared.Models.TaskBoard.TaskStatus>> GetStatuses() => await _optionsService.GetTaskStatuses();
    public async Task<IEnumerable<Shared.Models.TaskBoard.TaskPriority>> GetPriorities() => await _optionsService.GetTaskPriorities();

    public async Task<TaskEntry> SaveTask(TaskEntry entry)
    {
        entry.Status = null;
        entry.Priority = null;

        if (entry.Id == 0)
            entry = await _taskService.Create(entry);
        else
            entry = await _taskService.Update(entry);

        return await _taskService.Get(entry.Id);
    }
}
