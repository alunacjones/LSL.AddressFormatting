using System;

namespace LSL.AddressFormatting;

/// <summary>
/// Guard input data helpers
/// </summary>
public static class Guard
{
    /// <summary>
    /// Assert that the given value is not null
    /// </summary>
    /// <param name="parameterName"></param>
    /// <param name="value"></param>
    public static void AssertNotNull(string parameterName, object value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }
}
