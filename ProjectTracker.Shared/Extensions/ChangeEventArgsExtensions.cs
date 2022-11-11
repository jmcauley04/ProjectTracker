namespace ProjectTracker.Shared.Extensions;

public static class ChangeEventArgsExtensions
{
    public static int AsInt(this object? obj)
    {
        if (int.TryParse(obj?.ToString(), out int val))
            return val;
        return 0;
    }
}
