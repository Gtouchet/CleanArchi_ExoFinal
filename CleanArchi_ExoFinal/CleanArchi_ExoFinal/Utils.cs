using System.ComponentModel;
using System.Reflection;

namespace CleanArchi_ExoFinal;

public static class Utils
{
    public static E? ParseStringAs<E>(string value) where E : Enum
    {
        try
        {
            return (E)Enum.Parse(typeof(E), value, ignoreCase: true);
        }
        catch (Exception)
        {
            return default;
        }
    }

    public static string GetEnumDescription(Enum value)
    {
        FieldInfo? fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[]? attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
        if (attributes != null && attributes.Any())
        {
            return attributes.First().Description;
        }
        return value.ToString();
    }
}
