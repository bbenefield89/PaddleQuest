using PongCSharp.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace PongCSharp.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayAttributeOrDefault(this Enum enumValue, string propertyName)
    {
        var member = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault();

        var attribute = member?.GetCustomAttribute<DisplayAttribute>();
        if (attribute == null)
            return enumValue.ToString();

        var prop = typeof(DisplayAttribute).GetProperty(propertyName);
        if (prop == null)
            return enumValue.ToString();

        var value = prop.GetValue(attribute);
        return value?.ToString() ?? enumValue.ToString();
    }

    public static MatchType[] GetImplementedMatchTypesAsArray()
        => [MatchType.ScoreLimit];
}
