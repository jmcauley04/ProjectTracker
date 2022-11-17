using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Web;
using ProjectTracker.Shared.Attributes;

namespace ProjectTracker.Services;

[InjectService(InjectServiceAttribute.ServiceLifetime.Scoped)]
public class UserService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public UserService(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<string> GetUsername()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return authState.User.GetDisplayName() ?? authState.User.Identity?.Name ?? "Unknown";
    }

    public async Task<string> GetDisplayname()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var displayname = authState.User.Claims.SingleOrDefault(x => x.Type == "name")?.Value;
        return displayname ?? await GetUsername();
    }
}
