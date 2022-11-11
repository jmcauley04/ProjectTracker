using System.ComponentModel;
using System.Reflection;

namespace ProjectTracker.Shared.Extensions;

public static class EnumExtensions
{
    public static string GetDescription<T>(this T enumValue)
        where T : Enum
    {
        var memberInfo = typeof(T).GetMember(enumValue.ToString());

        if (memberInfo == null || memberInfo.Length == 0)
            return enumValue.ToString();

        var attr = memberInfo[0].GetCustomAttribute<DescriptionAttribute>();
        return attr?.Description ?? enumValue.ToString();
    }
}
