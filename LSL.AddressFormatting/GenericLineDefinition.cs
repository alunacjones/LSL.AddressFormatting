using System;
using System.Collections.Generic;

namespace LSL.AddressFormatting;

/// <summary>
/// A line definition that works against an instance of <typeparamref name="T"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class LineDefinition<T> : BaseLineDefinition<LineDefinition<T>>
{
    internal List<Func<T, string>> SectionProviders { get; } = [];

    internal LineDefinition(ICommonPropertiesAccessor builderContext) : base(builderContext) => _self = this;

    /// <summary>
    /// Adds section providers to use to create the address line
    /// </summary>
    /// <remarks>
    /// This can be called multiple times to keep adding section providers
    /// </remarks>
    /// <param name="sectionProviders"></param>
    /// <returns></returns>
    public LineDefinition<T> AddSectionProviders(IEnumerable<Func<T, string>> sectionProviders)
    {
        SectionProviders.AddRange(sectionProviders);
        return this;
    }
}