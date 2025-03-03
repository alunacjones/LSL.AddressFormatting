using System.Collections.Generic;

namespace LSL.AddressFormatting;

/// <summary>
/// Definition of an address line
/// </summary>
public sealed class LineDefinition : BaseLineDefinition<LineDefinition>
{
    internal List<string> Sections { get; } = [];

    internal LineDefinition(BuilderContext builderContext) : base(builderContext) => _self = this;

    /// <summary>
    /// Adds sections to use to create the address line
    /// </summary>
    /// <remarks>
    /// This can be called multiple times to keep adding sections
    /// </remarks>
    /// <param name="sections"></param>
    /// <returns></returns>
    public LineDefinition AddSections(IEnumerable<string> sections)
    {
        Sections.AddRange(sections);
        return this;
    }
}