using System;
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

        return context.LineDefinitions
            .Where(context.LineFilter)
            .Aggregate(
                new { Index = 0, Builder = new StringBuilder() }, 
                (agg, i) =>
                {
                    if (agg.Index > 0) agg.Builder.Append(context.LineSeparator);

                    agg.Builder.Append(string.Join(i.SectionSeparator, i.Sections.Where(context.SectionFilter)));

                    return new 
                    { 
                        Index = agg.Index + 1, 
                        agg.Builder 
                    };
                })
                .Builder
                .ToString();
    }

    /// <inheritdoc/>
    public Func<T, string> Create<T>(Action<IBuilderContext<T>> configurator)
    {
       var context = new BuilderContext<T>();
        configurator.Invoke(context);
        var result = new StringBuilder();

        return instance => context.LineDefinitions
            .Where(i => context.LineFilter(i, instance))
            .Aggregate(
                new { Index = 0, Builder = new StringBuilder() }, 
                (agg, i) =>
                {
                    if (agg.Index > 0) agg.Builder.Append(context.LineSeparator);

                    agg.Builder.Append(string.Join(
                        i.SectionSeparator, i.SectionProviders.Select(p => p(instance)).Where(context.SectionFilter))
                    );

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