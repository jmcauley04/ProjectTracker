using ProjectTracker.Shared.Models.Abstractions;

namespace ProjectTracker.Shared.Extensions
{
    public static class BaseSelectBoxEntryExtensions
    {
        public static string GetColor<T>(this T? t, IEnumerable<T>? items) where T : BaseSelectBoxEntry => t?.Color ?? items?.FirstOrDefault()?.Color ?? "gray";
    }
}
