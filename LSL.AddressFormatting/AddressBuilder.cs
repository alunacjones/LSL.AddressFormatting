using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSL.AddressFormatting;

/// <inheritdoc/>
public class AddressBuilder : IAddressBuilder
{
    /// <inheritdoc/>
    public string Build(Action<IBuilderContext> configurator)
    {
        var context = new BuilderContext();
        configurator.Invoke(context);
        var result = new StringBuilder();

        return InternalBuild(context, c => c.LineDefinitions.Where(context.LineFilter), l => l.Sections);
    }

    /// <inheritdoc/>
    public Func<T, string> Create<T>(Action<IBuilderContext<T>> configurator)
    {
        var context = new GenericBuilderContext<T>();
        configurator.Invoke(context);
        var result = new StringBuilder();
        
        return instance => InternalBuild(
            context,
            ctx => ctx.LineDefinitions.Where(i => context.LineFilter(i, instance)),
            l => l.SectionProviders.Select(x => x(instance))
        );
    }

    private string InternalBuild<TContext, TLineDefinition>(
        TContext context,
        Func<TContext, IEnumerable<TLineDefinition>> lineDefinitionsProvider,
        Func<TLineDefinition, IEnumerable<string>> sectionsProvider) 
        where TContext : ICommonPropertiesAccessor
        where TLineDefinition : ILineDefinition
    {
        var result = new StringBuilder();
        
        return lineDefinitionsProvider(context)
            .Aggregate(
                new { Index = 0, Builder = new StringBuilder() }, 
                (agg, lineDefinition) =>
                {
                    var filteredSections = sectionsProvider(lineDefinition).Where(lineDefinition.SectionFilter);

                    if (filteredSections.Any())
                    {
                        if (agg.Index > 0) agg.Builder.Append(context.LineSeparator);
                    
                        agg.Builder.Append(string.Join(
                            lineDefinition.SectionSeparator, 
                            filteredSections
                                .Select(i => lineDefinition.SectionValueTransformer(i))
                            )
                        );                        
                    }

                    return new 
                    { 
                        Index = agg.Index + 1, 
                        agg.Builder 
                    };
                })
                .Builder
                .ToString();        
    }
}