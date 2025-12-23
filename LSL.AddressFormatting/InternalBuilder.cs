using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSL.AddressFormatting;

internal static class InternalBuilder
{
    internal static string InternalBuild<TContext, TLineDefinition>(
        TContext context,
        Func<TContext, IEnumerable<TLineDefinition>> lineDefinitionsProvider,
        Func<TLineDefinition, IEnumerable<string>> sectionsProvider)
        where TContext : ICommonPropertiesAccessor
        where TLineDefinition : ILineDefinition => 
        lineDefinitionsProvider(context)
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
            .ToString()
            .ToNullIfEmpty(context.ReturnNullIfEmpty);    
}