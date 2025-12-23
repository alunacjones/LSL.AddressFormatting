using System;

namespace LSL.AddressFormatting;

/// <summary>
/// Base class for all LineDefinition classes
/// </summary>
/// <typeparam name="TFluentReturn"></typeparam>
public abstract class BaseLineDefinition<TFluentReturn> : ILineDefinition
{
    private string _sectionSeparator = null;
    private readonly ICommonPropertiesAccessor _builderContext;
    internal TFluentReturn _self;

    internal BaseLineDefinition(ICommonPropertiesAccessor builderContext)
    {
        _builderContext = builderContext;
    }
    
    internal ICommonPropertiesAccessor Parent => _builderContext;
    
    internal string SectionSeparator 
    { 
        get => _sectionSeparator ?? _builderContext.SectionSeparator;
        set => _sectionSeparator = value; 
    }

    internal Func<string, string> _sectionValueTransformer;

    internal Func<string, bool> _sectionFilter;

    internal Func<string, string> SectionValueTransformer
    {
        get => _sectionValueTransformer ?? _builderContext.SectionTransformer;
        set => _sectionValueTransformer = value;
    }

    internal Func<string, bool> SectionFilter
    {
        get => _sectionFilter ?? _builderContext.SectionFilter;
        set => _sectionFilter = value;
    }

    string ILineDefinition.SectionSeparator => SectionSeparator;
    Func<string, string> ILineDefinition.SectionValueTransformer => SectionValueTransformer;
    Func<string, bool> ILineDefinition.SectionFilter => SectionFilter;
    
    /// <summary>
    /// Sets the section separator for this line definition
    /// </summary>
    /// <remarks>
    /// If it is set to <c>null</c> then the parent 
    /// <see cref="IBuilderContext{T}"/>'s <c>SectionSeparator</c> will be used
    /// </remarks>
    /// <param name="sectionSeparator"></param>
    /// <returns></returns>
    public TFluentReturn WithSectionSeparator(string sectionSeparator)
    {
        SectionSeparator = sectionSeparator;
        return _self;
    }

    /// <summary>
    /// Allows for the transforming of each section value.
    /// </summary>
    /// <remarks>
    /// Can be used to trim values for example
    /// </remarks>
    /// <param name="sectionTransformer"></param>
    /// <returns></returns>
    public TFluentReturn WithSectionValueTransform(Func<string, string> sectionTransformer)
    {
        Guard.AssertNotNull(nameof(sectionTransformer), sectionTransformer);
        
        SectionValueTransformer = sectionTransformer;
        return _self;
    }

    /// <summary>
    /// Allows for custom section filtering.
    /// </summary>
    /// <remarks>
    /// The default behaviour is to return <c>true</c> if the 
    /// section value is not null and not empty.
    ///</remarks>
    /// <param name="sectionFilter"></param>
    /// <returns></returns>
    public TFluentReturn WithSectionFilter(Func<string, bool> sectionFilter)
    {
        Guard.AssertNotNull(nameof(sectionFilter), sectionFilter);

        SectionFilter = sectionFilter;
        return _self;
    }    
}
