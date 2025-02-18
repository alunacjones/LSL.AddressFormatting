using System;
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

/// <summary>
/// A line definition that works against an instance of <typeparamref name="T"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class LineDefinition<T>
{
    private readonly BuilderContext<T> _builderContext;
    private string _sectionSeparator = null;
    internal List<Func<T, string>> SectionProviders { get; } = [];

    internal string SectionSeparator 
    { 
        get => _sectionSeparator ?? _builderContext.SectionSeparator;
        private set => _sectionSeparator = value; 
    }

    internal LineDefinition(BuilderContext<T> builderContext)
    {
        _builderContext = builderContext;
    }

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

    /// <summary>
    /// Sets the section separator for this line definition
    /// </summary>
    /// <remarks>
    /// If it is set to <c>null</c> then the parent 
    /// <see cref="IBuilderContext"/>'s <c>SectionSeparator</c> will be used
    /// </remarks>
    /// <param name="sectionSeparator"></param>
    /// <returns></returns>
    public LineDefinition<T> WithSectionSeparator(string sectionSeparator)
    {
        SectionSeparator = sectionSeparator;
        return this;
    }    
}