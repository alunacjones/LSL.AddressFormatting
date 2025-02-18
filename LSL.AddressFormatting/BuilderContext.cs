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

internal class BuilderContext<T> : IBuilderContext<T>
{
    const string _defaultLineSeparator = ", ";
    const string _defaultSectionSeparator = " ";

    internal List<LineDefinition<T>> LineDefinitions { get; } = [];
    internal string SectionSeparator { get; private set; } = _defaultSectionSeparator;    
    internal Func<LineDefinition<T>, T, bool> LineFilter = new((ld, instance) => ld.SectionProviders.Any(v => !string.IsNullOrEmpty(v(instance))));
    internal Func<string, bool> SectionFilter = new(section => !string.IsNullOrEmpty(section));
    internal string LineSeparator { get; private set; } = _defaultLineSeparator;

    public IBuilderContext<T> AddLine(Action<LineDefinition<T>> definer)
    {
        var lineDefinition = new LineDefinition<T>(this);
        definer(lineDefinition);

        LineDefinitions.Add(lineDefinition);
        return this;
    }

    public IBuilderContext<T> WithLineSeparator(string lineSeparator)
    {
        LineSeparator = lineSeparator;
        return this;
    }

    public IBuilderContext<T> WithSectionSeparator(string sectionSeparator)
    {
        SectionSeparator = sectionSeparator;
        return this;
    }
}