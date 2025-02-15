using System;
using System.Collections.Generic;
using System.Linq;

namespace LSL.AddressFormatting;

internal class BuilderContext : IBuilderContext
{
    const string _defaultLineSeparator = ", ";
    const string _defaultSectionSeparator = " ";

    internal List<LineDefinition> LineDefinitions { get; } = [];
    internal string SectionSeparator { get; private set; } = _defaultSectionSeparator;    
    internal Func<LineDefinition, bool> LineFilter = new(ld => ld.Sections.Any(v => !string.IsNullOrEmpty(v)));
    internal Func<string, bool> SectionFilter = new(section => !string.IsNullOrEmpty(section));
    internal string LineSeparator { get; private set; } = _defaultLineSeparator;

    /// <inheritdoc/>
    public IBuilderContext AddLine(Action<LineDefinition> definer)
    {
        var lineDefinition = new LineDefinition(this);
        definer(lineDefinition);

        LineDefinitions.Add(lineDefinition);
        return this;
    }

    /// <inheritdoc/>
    public IBuilderContext WithSectionSeparator(string sectionSeparator)
    {
        SectionSeparator = sectionSeparator ?? _defaultSectionSeparator;
        return this;
    }

    /// <inheritdoc/>
    public IBuilderContext WithLineSeparator(string lineSeparator)
    {
        LineSeparator = lineSeparator ?? _defaultLineSeparator;
        return this;
    }
}
