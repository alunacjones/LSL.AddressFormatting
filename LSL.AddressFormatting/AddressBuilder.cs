using System;
using System.Linq;

namespace LSL.AddressFormatting;

/// <inheritdoc/>
public class AddressBuilder : IAddressBuilder
{
    /// <inheritdoc/>
    public IAddressFormatter<T> CreateFormatter<T>(Action<IBuilderContext<T>> configurator) => 
        new DelegatingAddressFormatter<T>(instance => InternalBuilder.InternalBuild(
            new GenericBuilderContext<T>().NullSafeConfigure(configurator),
            ctx => ctx.LineDefinitions.Where(i => ctx.LineFilter(i, instance)),
            l => l.SectionProviders.Select(x => x(instance))
        ));
}