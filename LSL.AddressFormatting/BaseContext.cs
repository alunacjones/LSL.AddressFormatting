using System;

namespace LSL.AddressFormatting;

internal abstract class BaseContext<TFluentReturn> : IBaseContext<TFluentReturn>, ICommonPropertiesAccessor
{
    const string _defaultLineSeparator = ", ";
    const string _defaultSectionSeparator = " ";
    internal TFluentReturn _self;

    internal string SectionSeparator { get; private set; } = _defaultSectionSeparator;    
    internal Func<string, bool> SectionFilter = new(section => !string.IsNullOrEmpty(section));
    internal string LineSeparator { get; private set; } = _defaultLineSeparator;
    internal Func<string, string> SectionTransformer { get; private set; } = s => s;

    Func<string, bool> ICommonPropertiesAccessor.SectionFilter => SectionFilter;
    string ICommonPropertiesAccessor.LineSeparator => LineSeparator;
    Func<string, string> ICommonPropertiesAccessor.SectionTransformer => SectionTransformer;
    string ICommonPropertiesAccessor.SectionSeparator => SectionSeparator;

    /// <inheritdoc/>
    public TFluentReturn WithSectionSeparator(string sectionSeparator)
    {
        SectionSeparator = sectionSeparator ?? _defaultSectionSeparator;
        return _self;
    }

    /// <inheritdoc/>
    public TFluentReturn WithLineSeparator(string lineSeparator)
    {
        LineSeparator = lineSeparator ?? _defaultLineSeparator;
        return _self;
    }

    /// <inheritdoc/>
    public TFluentReturn WithSectionValueTransform(Func<string, string> sectionTransformer)
    {
        Guard.AssertNotNull(nameof(sectionTransformer), sectionTransformer);
        
        SectionTransformer = sectionTransformer;
        return _self;
    }

    /// <inheritdoc/>
    public TFluentReturn WithSectionFilter(Func<string, bool> sectionFilter)
    {
        Guard.AssertNotNull(nameof(sectionFilter), sectionFilter);

        SectionFilter = sectionFilter;
        return _self;
    }
}
