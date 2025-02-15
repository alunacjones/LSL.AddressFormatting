using System.Collections.Generic;

namespace LSL.AddressFormatting;

/// <summary>
/// Definition of an address line
/// </summary>
public sealed class LineDefinition
{
    private readonly BuilderContext _builderContext;
    private string _sectionSeparator = null;
    internal List<string> Sections { get; } = [];

    internal string SectionSeparator 
    { 
        get => _sectionSeparator ?? _builderContext.SectionSeparator;
        private set => _sectionSeparator = value; 
    }

    internal LineDefinition(BuilderContext builderContext)
    {
        _builderContext = builderContext;
    }
    
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

    /// <summary>
    /// Sets the section separator for this line definition
    /// </summary>
    /// <remarks>
    /// If it is set to <c>null</c> then the parent 
    /// <see cref="IBuilderContext"/>'s <c>SectionSeparator</c> will be used
    /// </remarks>
    /// <param name="sectionSeparator"></param>
    /// <returns></returns>
    public LineDefinition WithSectionSeparator(string sectionSeparator)
    {
        SectionSeparator = sectionSeparator;
        return this;
    }
}