namespace SSB.Shared.Extensions;

public static class GenericExtensions
{
    /// <summary>
    /// Determines whether a value is contained in list of params.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="params">List of values to compare.</param>
    /// <returns></returns>
    public static bool In<T>(this T value, params T[] @params)
    {
        ArgumentNullException.ThrowIfNull(@params);
        return @params.ToHashSet().Contains(value);
    }
}
