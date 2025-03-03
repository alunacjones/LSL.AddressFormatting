using System;
using System.Collections.Generic;
using System.Linq;

namespace LSL.AddressFormatting;

internal sealed class BuilderContext : BaseContext<IBuilderContext>, IBuilderContext
{
    public BuilderContext() => _self = this;

    internal List<LineDefinition> LineDefinitions { get; } = [];
    internal Func<LineDefinition, bool> LineFilter = new(ld => ld.Sections.Any(v => ld.Parent.SectionFilter(v)));

    /// <inheritdoc/>
    public IBuilderContext AddLine(Action<LineDefinition> definer)
    {
        var lineDefinition = new LineDefinition(this);
        definer(lineDefinition);

        LineDefinitions.Add(lineDefinition);
        return this;
    }
}
