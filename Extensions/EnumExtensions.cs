using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace PongCSharp.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayNameOrDefault(this Enum enumValue)
    {
        var displayName = enumValue
            .GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault()?
            .GetCustomAttribute<DisplayAttribute>()?
            .Name;

        return displayName ?? enumValue.ToString();
    }
}
