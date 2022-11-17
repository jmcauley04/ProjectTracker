using ProjectTracker.DataAccess.Services;
using ProjectTracker.Shared.Attributes;
using ProjectTracker.Shared.Models;

namespace ProjectTracker.Controllers;

[InjectService(InjectServiceAttribute.ServiceLifetime.Scoped)]
public class OverviewController
{
    private readonly ProjectEventService _projectEventService;

    public OverviewController(ProjectEventService projectEventService)
    {
        _projectEventService = projectEventService;
    }

    public async Task<IEnumerable<ProjectEvent>> GetEvents() => await _projectEventService.GetAll();
}
