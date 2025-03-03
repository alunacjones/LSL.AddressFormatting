using System;
using System.Collections.Generic;
using System.Linq;

namespace LSL.AddressFormatting;

internal sealed class GenericBuilderContext<T> : BaseContext<IBuilderContext<T>>, IBuilderContext<T>
{
    public GenericBuilderContext() => _self = this;

    internal List<LineDefinition<T>> LineDefinitions { get; } = [];
    internal Func<LineDefinition<T>, T, bool> LineFilter = new((ld, instance) => ld.SectionProviders.Any(v => ld.Parent.SectionFilter(v(instance))));

    public IBuilderContext<T> AddLine(Action<LineDefinition<T>> definer)
    {
        var lineDefinition = new LineDefinition<T>(this);
        definer(lineDefinition);

        LineDefinitions.Add(lineDefinition);
        return this;
    }
}