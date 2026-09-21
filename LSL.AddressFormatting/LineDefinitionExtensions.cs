using System;
using System.Linq;

namespace LSL.AddressFormatting;

/// <summary>
/// Line definition extensions
/// </summary>
public static class LineDefinitionExtensions
{
    /// <summary>
    /// Adds section providers to use to create the address line
    /// </summary>
    /// <remarks>
    /// This can be called multiple times to keep adding section providers
    /// </remarks>
    /// <param name="source"></param>
    /// <param name="sectionProviders"></param>
    /// <returns></returns>

    public static LineDefinition<T> AddSectionProviders<T>(this LineDefinition<T> source, params Func<T, string>[] sectionProviders) =>
        source.AddSectionProviders(sectionProviders.AsEnumerable());
}