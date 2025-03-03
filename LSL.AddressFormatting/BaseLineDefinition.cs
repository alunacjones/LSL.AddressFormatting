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

    string ILineDefinition.SectionSeparator => SectionSeparator;

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
}
