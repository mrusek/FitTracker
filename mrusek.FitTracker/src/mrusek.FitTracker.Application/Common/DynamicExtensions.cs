using System.Reflection;

namespace mrusek.FitTracker.Application.Common;

public static class DynamicExtensions
{
    public static T? GetPropertyValue<T>(object instance, string propertyName)
    {
        ArgumentNullException.ThrowIfNull(instance);

        var property = instance.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

        if (property == null)
            return default;

        var value = property.GetValue(instance);

        if (value is T typedValue)
            return typedValue;

        try
        {
            return (T)Convert.ChangeType(value, typeof(T))!;
        }
        catch
        {
            return default;
        }
    }
}